using KhoaHocApi.Application.Payloads.ResponseModel.DataStudents;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper.StudentConverter
{
    public class MakeQuestionConerter
    {
        protected readonly IBaseRepository<User> _userRepository;
        protected readonly IBaseRepository<SubjectDetail> _subjectDetailRepository;
        protected readonly IBaseRepository<Answers> _AnswerRepository;
        protected readonly AnswerConverter _answerConverter;
        public MakeQuestionConerter( IBaseRepository<SubjectDetail> subjectDetailRepository, IBaseRepository<User> baseRepository, 
            IBaseRepository<Answers> answerRepository ,AnswerConverter answerConverter)
        {
            _subjectDetailRepository = subjectDetailRepository;
            _userRepository = baseRepository;
            _AnswerRepository = answerRepository;
            _answerConverter = answerConverter;
        }
        public DataResponseMakeQuestion EntityDTO(MakeQuestion makeQuestion)
        {
            var username = _userRepository.GetByIdAsync(makeQuestion.UserId).Result;
            var subjectDetail =_subjectDetailRepository.GetByIdAsync(makeQuestion.SubjectDetailId).Result;
            return new DataResponseMakeQuestion()
            {
                userId = makeQuestion.UserId,
                Id=makeQuestion.Id,
                Question = makeQuestion.Question,
                NumberOfAnswer = makeQuestion.NumberOfAnswer,
                CreateTime = makeQuestion.CreateTime,
                UpdateTime = makeQuestion.UpdateTime,
                UserName = username.Fullname,
                Avatar = username.Avatar,
                SubjectDetailName =subjectDetail.SubjectDetailName,
                DataResponseAnswer = _AnswerRepository.GetAllAsync(x => x.MakeQuestionId == makeQuestion.Id).Result.Select(x => _answerConverter.EntityDTO(x))
            };
        }

    }
}
