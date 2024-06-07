using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.StudentsRequest
{
    public class Request_CreateLog
    {
        public string Content { get; set; }
        public string Title { get; set; }

    }
}
