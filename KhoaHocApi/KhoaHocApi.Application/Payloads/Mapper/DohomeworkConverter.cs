using KhoaHocApi.Application.Payloads.Mapper.PracticeConverter;
using KhoaHocApi.Application.Payloads.ResponseModel.DataDohomework;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.Enumerates;
using KhoaHocApi.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper
{
    public class DohomeworkConverter
    {
        private readonly IBaseRepository<Practice> _practicRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<RegisterStudy> _registerRepository;
        private readonly PractiveConverter _practiceConverter;
        private readonly UserConverter _userConverter;
        public DohomeworkConverter(IBaseRepository<Practice> practicRepository, IBaseRepository<User> userRepository, IBaseRepository<RegisterStudy> registerRepository, PractiveConverter practiceConverter, UserConverter userConverter)
        {
            _practicRepository = practicRepository;
            _userRepository = userRepository;
            _registerRepository = registerRepository;
            _practiceConverter = practiceConverter;
            _userConverter = userConverter;
        }
        public DataResponseDohomework EntityDTO(DoHomework doHomework)
        {
            var practices = _practicRepository.GetByIdAsync(doHomework.PracticeId).Result;
            var users = _userRepository.GetByIdAsync(doHomework.UserId).Result;
            var register = _registerRepository.GetByIdAsync(doHomework.RegisterStudyId).Result;
            return new DataResponseDohomework()
            {
                Id=doHomework.Id,
                PracticeID = practices.Id,
                UserName = users.Fullname,
                RegisterStudyId = register.Id,
                HomeworkStatus = ConstantEnum.HomeworkEnum.Finished,
                IsFinished = doHomework.IsFinished,
                ActualOutput = doHomework.ActualOutput,
                DoneTime = doHomework.DoneTime,
            };
        }
    }
}

