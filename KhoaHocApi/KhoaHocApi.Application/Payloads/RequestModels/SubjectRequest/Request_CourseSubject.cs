using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.SubjectRequest
{
    public class Request_CourseSubject
    {
        public long CourseId { get; set; }
        public long SubjectId { get; set; }
    }
}
