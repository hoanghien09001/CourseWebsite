using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataSubject
{
    public class DataResponseSubjectDetails
    {
        public long SubjectDetailId {  get; set; }
        public string SubjectDetailName { get; set; }
        public bool IsFinished { get; set; }
        public string LinkVideo { get; set; }
        public bool IsActive { get; set; }
        public string SubjectName { get; set; }
    }
}
