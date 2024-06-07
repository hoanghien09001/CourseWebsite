using KhoaHocApi.Application.Payloads.ResponseModel.DataUsers;
using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper
{
    public class UserConverter
    {
        public DataResponseUser EntityToDTO(User user)
        {
            
            return new DataResponseUser()
            {
                Address = user.Address,
                Avatar = user.Avatar,
                CreateTime = user.CreateTime,
                DateOfBirth = user.DateOfBirth.Value.Date,
                Email = user.Email,
                Fullname = user.Fullname,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status.ToString(),
                UpdateTime = user.UpdateTime
            };
        }
    }
}
