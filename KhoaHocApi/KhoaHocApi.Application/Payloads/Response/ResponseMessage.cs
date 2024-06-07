using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Response
{
    public static class ResponseMessage
    {
        public static string GetEmailSuccess(string email)
        {
            return $"Email đã được gửi đến: {email}";
        }
    }
}
