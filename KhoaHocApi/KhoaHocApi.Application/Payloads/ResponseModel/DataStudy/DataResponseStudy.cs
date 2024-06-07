using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataStudy
{
    public class DataResponseStudy
    {
        public bool IsFinished { get; set; }
        public DateTime RegisterTime { get; set; }
        public int PercentComplete { get; set; }
        public DateTime DoneTime { get; set; }
        public bool IsActive { get; set; }
        public long UserId { get; set; }     
        public string UserName { get; set; }
        public long CourseId { get; set; }
        public string CourseName { get; set; }
        public long SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}
