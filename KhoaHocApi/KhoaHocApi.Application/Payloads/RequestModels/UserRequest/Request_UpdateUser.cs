using KhoaHocApi.Domain.Enumerates;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.UserRequest
{
    public class Request_UpdateUser
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile Avatar { get; set; }
        public string Fullname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
