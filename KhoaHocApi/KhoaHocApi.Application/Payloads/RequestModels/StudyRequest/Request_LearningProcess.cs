using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.StudyRequest
{
    public class Request_LearningProcess
    {
        public long UserId { get; set; }       
        public long RegisterStudyId { get; set; }       
        public long SubjectId { get; set; }        
    }
}
