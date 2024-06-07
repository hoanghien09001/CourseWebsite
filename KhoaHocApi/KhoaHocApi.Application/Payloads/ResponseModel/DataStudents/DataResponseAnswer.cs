using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataStudents
{
    public class DataResponseAnswer:DataResponseBase
    { 
        public string UserName { get; set; }
        public string Avatar {  get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        
       

    }
}
