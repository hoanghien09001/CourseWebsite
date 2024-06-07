using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataStudents
{
    public class DataResponseLikeBlog : DataResponseBase
    {
        public string UserName { get; set; }
        public string BlogTitle { get; set; }
    }
}
