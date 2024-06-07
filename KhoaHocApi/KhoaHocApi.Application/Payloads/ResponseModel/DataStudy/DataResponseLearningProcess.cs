using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataStudy
{
    public class DataResponseLearningProcess
    {
        public long UserId { get; set; }
        public long RegisterStudyId { get; set; }
        public long SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}
