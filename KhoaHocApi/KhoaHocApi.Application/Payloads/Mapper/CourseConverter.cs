using KhoaHocApi.Application.Payloads.ResponseModel.DataCourse;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper
{
    public class CourseConverter
    {
        private readonly IBaseRepository<User> _userBaseRepository;

        public CourseConverter(IBaseRepository<User> userBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
        }

        public DataResponseCourse EntityDTO(Course course)
        {
            var user =  _userBaseRepository.GetByIdAsync(course.UserId).Result;
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
                UserName = user.Fullname,
            };
        }
    }
}
