﻿using KhoaHocApi.Application.Payloads.ResponseModel.DataUsers;
using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper
{
    public class TeacherConverter
    {
        public DataResponseTeacher EntityToDTO(User user)
        {

            return new DataResponseTeacher()
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
                UpdateTime = user.UpdateTime,
                CertificateId = user.CertificateId,
                Certificate = user.Certificate,
            };
        }
    }
}
