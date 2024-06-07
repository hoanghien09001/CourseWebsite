using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataStudents
{
    public class DataResponseMakeQuestion:DataResponseBase
    {
        public long userId {  get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string SubjectDetailName { get; set; }
        public string Question { get; set; }
        public int NumberOfAnswer { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public IQueryable<DataResponseAnswer> DataResponseAnswer { get; set; }

    }
}
