using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataPractice
{
    public class DataResponsePractice:DataResponseBase
    {
        public string Level { get; set; }
        public string PracticeCode { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string ExpectOutput { get; set; }
        public bool IsRequired { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public double MediumScore { get; set; }
        public string SubjectDetailName{ get; set; }
        public string ProgramingLannguageName { get; set; }

    }
}
