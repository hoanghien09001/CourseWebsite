using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.UserRequest
{
    public class Request_CreateNewPassword
    {
        public string ConfirmCode {  get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword {  get; set; }
    }
}
