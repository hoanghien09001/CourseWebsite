using KhoaHocApi.Application.Payloads.ResponseModel.DataStudy;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper.StudyConverter
{
    public class StudyConverter
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Course> _baseCourseRepository;
        private readonly IBaseRepository<Subject> _baseSubjectRepository;
        public StudyConverter(IBaseRepository<User> baseUserRepository, IBaseRepository<Course> baseCourseRepository, IBaseRepository<Subject> baseSubjectRepository)
        {
            _baseUserRepository = baseUserRepository;
            _baseCourseRepository = baseCourseRepository;
            _baseSubjectRepository = baseSubjectRepository;
        }
        public DataResponseStudy EntityDTO(RegisterStudy registerStudy)
        {
            return new DataResponseStudy
            {
                IsFinished = registerStudy.IsFinished,
                RegisterTime = registerStudy.RegisterTime,
                PercentComplete = registerStudy.PercentComplete,
                DoneTime = registerStudy.DoneTime,
                IsActive = registerStudy.IsActive,
                UserId = registerStudy.UserId,
                UserName = _baseUserRepository.GetByIdAsync(registerStudy.UserId).Result.Username,
                CourseId = registerStudy.CourseId,
                CourseName = _baseCourseRepository.GetByIdAsync(registerStudy.CourseId).Result.CourseName,
                SubjectId = registerStudy.SubjectId,
                SubjectName = _baseSubjectRepository.GetByIdAsync(registerStudy.SubjectId).Result.SubjectName
            };
        }
    }
}
