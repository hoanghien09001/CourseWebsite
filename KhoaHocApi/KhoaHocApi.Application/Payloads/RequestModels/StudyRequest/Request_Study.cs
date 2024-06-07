using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.StudyRequest
{
    public class Request_Study
    {
        public long UserId { get; set; }
        public long CourseId { get; set; }
        public long SubjectId { get; set; } //Môn học hiện tại
    }
}
