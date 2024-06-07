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
    public  class AnswerConverter
    {
        protected readonly IBaseRepository<User> _userRepository;
        protected readonly IBaseRepository<MakeQuestion> _questionRepository;
        public AnswerConverter( IBaseRepository<MakeQuestion> questionRepository , IBaseRepository<User> userRepository)
        {
            _questionRepository = questionRepository;
            _userRepository = userRepository;
        }
        public DataResponseAnswer EntityDTO(Answers answers)
        {
            var username = _userRepository.GetByIdAsync(answers.UserId).Result;
            var question = _questionRepository.GetByIdAsync(answers.MakeQuestionId).Result;
            return new DataResponseAnswer()
            {
                Id=answers.Id,
                UserName = username.Fullname,
                Question = question.Question,
                Answer = answers.Answer,
                CreateTime = answers.CreateTime,
                UpdateTime = answers.UpdateTime,
                Avatar = username.Avatar
            };
        }
    }
}
