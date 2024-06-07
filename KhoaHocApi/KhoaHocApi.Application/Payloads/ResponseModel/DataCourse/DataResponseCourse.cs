using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataCourse
{
    public class DataResponseCourse
    {
        public string CourseName { get; set; }
        public string Introduce { get; set; }
        public string ImageCourse { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public long UserId { get; set; }
        public int TotalCourseDuration { get; set; }
        public int NumberOfStudent { get; set; }
        public int NumberOfPurchases { get; set; }
        public long CourseId {  get; set; }
    }
}
