using KhoaHocApi.Application.Payloads.ResponseModel.DataCourse;
using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper
{
    public class CourseConverter
    {
        public DataResponseCourse EntityDTO(Course course)
        {
            return new DataResponseCourse()
            {
                CourseName = course.CourseName,
                Introduce = course.Introduce,
                ImageCourse = course.ImageCourse,
                Code = course.Code,
                Price = course.Price,
                UserId = course.UserId,
                NumberOfPurchases = course.NumberOfPurchases,
                NumberOfStudent = course.NumberOfStudent,
                TotalCourseDuration = course.TotalCourseDuration,
                CourseId = course.Id,
            };
        }
    }
}
