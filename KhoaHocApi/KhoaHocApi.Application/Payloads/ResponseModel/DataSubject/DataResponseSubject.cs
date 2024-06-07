using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataSubject
{
    public class DataResponseSubject
    {
        public long SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Symbol { get; set; } //Ký hiệu
        public bool IsActive { get; set; }
    }
}
