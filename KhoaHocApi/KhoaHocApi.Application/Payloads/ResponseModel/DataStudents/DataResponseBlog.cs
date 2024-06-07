using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataStudents
{
    public class DataResponseBlog:DataResponseBase
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfComments { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string  UserName { get; set; }
        public IQueryable<DataResponseComment> DataResponseComments { get; set; }

        public IQueryable<DataResponseLikeBlog> DataResponseLikes { get; set; }

    }
}
