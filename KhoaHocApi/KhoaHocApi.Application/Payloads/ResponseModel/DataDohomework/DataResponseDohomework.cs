using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataDohomework
{
    public class DataResponseDohomework:DataResponseBase
    {
        public long PracticeID  { get; set; }
        public string UserName { get; set; }
        public long RegisterStudyId { get; set; }
        public ConstantEnum.HomeworkEnum HomeworkStatus { get; set; }
        public bool IsFinished { get; set; }
        public string ActualOutput { get; set; }
        public DateTime DoneTime { get; set; }
        

    }
}
