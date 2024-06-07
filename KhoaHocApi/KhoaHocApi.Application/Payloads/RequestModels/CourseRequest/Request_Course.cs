using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.RequestModels.CourseRequest
{
    public class Request_Course
    {
        public string CourseName { get; set; }
        public string? Introduce { get; set; }
        public IFormFile? ImageCourse { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public int TotalCourseDuration { get; set; }

    }
}
