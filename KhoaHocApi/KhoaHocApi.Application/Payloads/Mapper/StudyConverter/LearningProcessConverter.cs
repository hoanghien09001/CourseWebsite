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
    public class LearningProcessConverter
    {
        private readonly IBaseRepository<Subject> _baseSubjectRepository;
        public LearningProcessConverter(IBaseRepository<Subject> baseSubjectRepository)
        {
            _baseSubjectRepository = baseSubjectRepository;
        }
        public DataResponseLearningProcess EntityDTO(LearningProgress learningProgress)
        {
            return new DataResponseLearningProcess
            {
                UserId = learningProgress.UserId,
                RegisterStudyId = learningProgress.RegisterStudyId,
                SubjectId = learningProgress.SubjectId,
                SubjectName = _baseSubjectRepository.GetByIdAsync(learningProgress.SubjectId).Result.SubjectName
            };
        }
    }
}
