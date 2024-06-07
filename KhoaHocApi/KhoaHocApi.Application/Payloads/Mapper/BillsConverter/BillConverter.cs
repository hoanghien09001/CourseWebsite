using KhoaHocApi.Application.Payloads.ResponseModel.DataBill;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper.BillsConverter
{
    public class BillConverter
    {
        protected readonly IBaseRepository<User> _userRepository;
        protected readonly IBaseRepository<Course> _courseRepository;
        protected readonly IBaseRepository<BillStatus> _statusRepository;
        public BillConverter( IBaseRepository<Course> courseRepository, IBaseRepository<BillStatus> statusRepository, IBaseRepository<User> userRepository)
        {
            _courseRepository = courseRepository;
            _statusRepository = statusRepository;
            _userRepository = userRepository;
        }
        public DataResponseBill EntityDTO(Bill bill)
        {
            var username = _userRepository.GetByIdAsync(bill.UserId).Result;
            var coursename = _courseRepository.GetByIdAsync(bill.CourseId).Result;
            var billstatus = _statusRepository.GetByIdAsync(bill.BillStatusId).Result;
            return new DataResponseBill()
            {
                Id=bill.Id,
                UserName =username.Fullname,
                CourseName= coursename.CourseName,
                Price= bill.Price,
                TradingCode=bill.TradingCode,
                BillStatus= billstatus.BillStatusName,
                CreateTime= bill.CreateTime,
            };
        }

    }
}
