using KhoaHocApi.Application.Payloads.ResponseModel.DataSubject;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper.SubjectConverter
{
    public class CourseSubjectConverter
    {
        private readonly IBaseRepository<Course> _courseRepository;
        private readonly IBaseRepository<Subject> _subjectRepository;
        public CourseSubjectConverter(IBaseRepository<Course> courseRepository, IBaseRepository<Subject> subjectRepository)
        {
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
        }

        public DataResponseCourseSubject EntityDTO(CourseSubject courseSubject)
        {          
            return new DataResponseCourseSubject
            {
                CourseName = _courseRepository.GetByIdAsync(courseSubject.CourseId).Result.CourseName,
                SubjectName = _subjectRepository.GetByIdAsync(courseSubject.SubjectId).Result.SubjectName
            };
        }
    }
}
