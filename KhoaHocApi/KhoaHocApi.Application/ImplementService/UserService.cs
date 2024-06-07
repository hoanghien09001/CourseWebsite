using KhoaHocApi.Application.Handle.HandleFile;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.Mapper;
using KhoaHocApi.Application.Payloads.RequestModels.UserRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataUsers;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.ImplementService
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _repository;
        private readonly UserConverter _converter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IBaseRepository<User> repository, UserConverter converter, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _converter = converter;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseObject<DataResponseUser>> UpdateUser(long userId, Request_UpdateUser request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa được xác thực",
                        Data = null,
                    };
                }
                var userItem=await _repository.GetByIdAsync(userId);
                var user = currentUser.FindFirst("Id").Value;
                if (long.Parse(user) != userId && long.Parse(user)!=userItem.Id)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Message = "Bạn không có quyền thực hiện chức năng này",
                        Data = null,
                    };
                } 
                userItem.Avatar = await HandleUploadFile.Upfile(request.Avatar);
                userItem.PhoneNumber = request.PhoneNumber;
                userItem.DateOfBirth = request.DateOfBirth;
                userItem.Email = request.Email;
                userItem.UpdateTime = DateTime.Now;
                userItem.Fullname = request.Fullname;
                userItem.Address = request.Address;
                await _repository.UpdateAsync(userItem);
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Cập nhật thông tin thành công",
                    Data = _converter.EntityToDTO(userItem)
                };
            }catch (Exception ex)
            {
                throw;
            }
        }
    }
}
