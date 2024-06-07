using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.SubjectRequest
{
    public class Request_SubjectDetails
    {
        public string SubjectDetailName { get; set; }     
        public string LinkVideo { get; set; }       
        public long SubjectId { get; set; }
    }
}
