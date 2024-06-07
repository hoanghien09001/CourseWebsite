using BaiTestPost.Handler.Pagination;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.Mapper.BillsConverter;
using KhoaHocApi.Application.Payloads.Mapper.StudentConverter;
using KhoaHocApi.Application.Payloads.RequestModels.UserRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataBill;
using KhoaHocApi.Application.Payloads.ResponseModel.DataStudents;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.DataContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.ImplementService
{
    public class BillService : IBillService
    {

        private readonly IHttpContextAccessor _httpContext;
        private readonly BillConverter _billConverter;
        private readonly IBaseRepository<Bill> _billRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Course> _courseRepository;
        public BillService ( IHttpContextAccessor httpContext, BillConverter billConverter , IBaseRepository<Bill> billRepository , IBaseRepository<User> userRepository
            , IBaseRepository<Course> courseRepository)
        {

            _httpContext = httpContext;
            _billConverter = billConverter;
            _billRepository = billRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }
        #region//CreateBills (Thêm dữ liệu vào bảng statusBill để chạy code)
        public async Task<ResponseObject<DataResponseBill>> CreateBill(long CourseID)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseBill>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin =await _userRepository.GetByIdAsync(idUser) ;
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseBill>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }
            var course =await _courseRepository.GetByIdAsync(CourseID);
            if (course == null)
            {
                return new ResponseObject<DataResponseBill>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Course không tồn tại!",
                    Data = null,
                };
            }
            Bill bill = new Bill()
            {
                UserId = idUser,
                CourseId = CourseID,
                BillStatusId = 1,
                Price = course.Price,
                TradingCode = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                CreateTime = DateTime.Now,
            };
            await _billRepository.CreateAsync(bill);
            return new ResponseObject<DataResponseBill>
            {
                Status = StatusCodes.Status200OK,
                Message = "Tạo hóa đơn thành công!",
                Data = _billConverter.EntityDTO(bill),
            };
        }
        #endregion


        public async Task<PageResult<DataResponseBill>> GetAllBill(int pageNumber, int pageSize)
        {
          var currentUser =_httpContext.HttpContext.User;
      
            if (!currentUser.Identity.IsAuthenticated)
            {
                return null;
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            if (currentUser.IsInRole("User"))
            {
                var list = await _billRepository.GetAllAsync(x => x.UserId == idUser);
                var query = list.ToList().Select(item => _billConverter.EntityDTO(item)).OrderByDescending(y => y.CreateTime);
                var result = Pagination.GetPagedData(query.AsQueryable(), pageNumber, pageSize);
                return result;
            }
            else 
            {
                var list = await _billRepository.GetAllAsync();
                var query = list.ToList().Select(item => _billConverter.EntityDTO(item)).OrderByDescending(y => y.CreateTime);
                var result = Pagination.GetPagedData(query.AsQueryable(), pageNumber, pageSize);
                return result;
            }
            return null;
        }

        public async Task<ResponseObject<DataResponseBill>> GetAllBillById(long BillID)
        {

            var bill = await _billRepository.GetByIdAsync(BillID);

            if (bill == null)
            {
                return new ResponseObject<DataResponseBill>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Không tồn tại Bill !",
                    Data = null
                };
            }

            var billDto = _billConverter.EntityDTO(bill);

            return new ResponseObject<DataResponseBill>
            {
                Status = StatusCodes.Status200OK,
                Message = "Hiển thị bill thành công!",
                Data = billDto
            };

        }

        
    }
}
