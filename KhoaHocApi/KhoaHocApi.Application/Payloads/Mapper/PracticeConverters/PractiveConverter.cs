using KhoaHocApi.Application.Payloads.ResponseModel.DataPractice;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.Enumerates;
using KhoaHocApi.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper.PracticeConverter
{
    public class PractiveConverter
    {
        private readonly IBaseRepository<SubjectDetail> _detailRepository;
        private readonly IBaseRepository<ProgramingLanguage> _programRepository;
        public PractiveConverter(IBaseRepository<SubjectDetail> detailRepository, IBaseRepository<ProgramingLanguage> programRepository)
        {
            _detailRepository = detailRepository;
            _programRepository = programRepository;
        }
        public DataResponsePractice EntityDTO(Practice practice)
        {
            var subjectdetail = _detailRepository.GetByIdAsync(practice.SubjectDetailId).Result;
            var program = _programRepository.GetByIdAsync(practice.ProgramingLannguageId).Result;
            return new DataResponsePractice()
            {
                Id=practice.Id,
                Level = practice.Level.ToString(),
                PracticeCode = practice.PracticeCode,
                Title = practice.Title,
                Topic = practice.Topic,
                ExpectOutput = practice.ExpectOutput,
                CreateTime = practice.CreateTime,
                UpdateTime = practice.UpdateTime,
                MediumScore = practice.MediumScore,
                IsRequired=practice.IsRequired,
                SubjectDetailName = subjectdetail.SubjectDetailName,
                ProgramingLannguageName = program.LanguageName,
            };
        }
    }
}
