using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataUsers
{
    public class DataResponseTeacher : DataResponseBase
    {
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Fullname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string Status { get; set; }
        public long? CertificateId { get; set; }
        public Certificate? Certificate { get; set; }
    }
}
