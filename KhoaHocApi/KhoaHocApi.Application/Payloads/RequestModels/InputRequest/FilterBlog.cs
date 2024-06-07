using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.InputRequest
{
    public class FilterBlog
    {
        public long? UserId { get; set; }
        public string? Title { get; set; }
    }
}
