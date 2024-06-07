using KhoaHocApi.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.InputRequest
{
    public class FilterPractice
    {
        public long? SubjectDetailId { get; set; }
        public long? ProgramingLannguageId { get; set; }
        public ConstantEnum.LevelEnum? Level { get; set; }
        public string? PracticeCode { get; set; }
        public string? Title { get; set; }
        public string? Topic { get; set; }
        public string? ExpectOutput { get; set; }
    }
}
