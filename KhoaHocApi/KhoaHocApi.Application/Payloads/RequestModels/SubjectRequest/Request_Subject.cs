using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.SubjectRequest
{
    public class Request_Subject
    {
        public string SubjectName { get; set; }
        public string Symbol { get; set; } //Ký hiệu
    }
}
