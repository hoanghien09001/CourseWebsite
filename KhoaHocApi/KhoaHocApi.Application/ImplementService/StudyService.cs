
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.Mapper.StudyConverter;
using KhoaHocApi.Application.Payloads.RequestModels.StudyRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataStudy;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.ImplementService
{
    public class StudyService : IStudyService
    {
        private readonly IBaseRepository<RegisterStudy> _baseRegisterRepository;
        private readonly IBaseRepository<LearningProgress> _baseLearningRepository;
        private readonly StudyConverter _studyConverter;
        private readonly LearningProcessConverter _learningConverter;
        public StudyService(IBaseRepository<RegisterStudy> baseRegisterRepository, IBaseRepository<LearningProgress> baseLearningRepository,StudyConverter studyConverter, LearningProcessConverter learningConverter)
        {
            _baseRegisterRepository = baseRegisterRepository;
            _baseLearningRepository = baseLearningRepository;
            _studyConverter = studyConverter;
            _learningConverter = learningConverter;
        }
        //RegisterStudy
        public async Task<IQueryable<DataResponseStudy>> GetAllRegisterStudy()
        {
            var registerList = await _baseRegisterRepository.GetAllAsync();
            var converterRegisterList = registerList.Select(item => _studyConverter.EntityDTO(item));
            return converterRegisterList.AsQueryable();
        }

        public async Task<ResponseObject<DataResponseStudy>> GetRegisterStudyById(long registerId)
        {
            var currentRegister = await _baseRegisterRepository.GetByIdAsync(registerId);
            if (currentRegister == null)
            {
                return new ResponseObject<DataResponseStudy>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Không tìm thấy đăng kí này!",
                    Data = null
                };
            }
            await _baseRegisterRepository.GetByIdAsync(registerId);
            return new ResponseObject<DataResponseStudy>
            {
                Status = StatusCodes.Status200OK,
                Message = "Đăng kí cần tìm",
                Data = _studyConverter.EntityDTO(currentRegister)
            };
        }
        public async Task<ResponseObject<DataResponseStudy>> AddRegisterSudy(long userId,Request_Study request)
        {
            var user = await _baseRegisterRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ResponseObject<DataResponseStudy>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Người dùng chưa đăng nhập",
                    Data = null
                };
            }
            var registerStudy = new RegisterStudy
            {
                UserId = request.UserId,
                CourseId = request.CourseId,
                SubjectId = request.SubjectId,
                IsFinished = false, 
                IsActive = false,
                RegisterTime = DateTime.Now,
                DoneTime = DateTime.Now,
                PercentComplete = 0
            };
            await _baseRegisterRepository.CreateAsync(registerStudy);
            registerStudy.IsActive = true;
            await _baseRegisterRepository.UpdateAsync(registerStudy);
            return new ResponseObject<DataResponseStudy>
            {
                Status = StatusCodes.Status200OK,
                Message = "Đăng kí học thành công!",
                Data = _studyConverter.EntityDTO(registerStudy)
            };
        }

        public async Task<ResponseObject<DataResponseStudy>> DeleteRegisterStudy(long registerId)
        {
            var currentRegister = await _baseRegisterRepository.GetByIdAsync(registerId);
            if(currentRegister == null)
            {
                return new ResponseObject<DataResponseStudy>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Không tìm thấy đăng kí này!",
                    Data = null
                };
            }
            await _baseRegisterRepository.DeleteAsync(registerId);
            return new ResponseObject<DataResponseStudy>
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa đăng kí thành công!",
                Data = _studyConverter.EntityDTO(currentRegister)
            };
        }     
        public async Task<ResponseObject<DataResponseStudy>> UpdateRegisterStudy(long registerId, Request_Study request)
        {
            var currentRegister = await _baseRegisterRepository.GetByIdAsync(registerId);
            if (currentRegister == null)
            {
                return new ResponseObject<DataResponseStudy>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Không tìm thấy đăng kí này!",
                    Data = null
                };
            }
            currentRegister.CourseId = request.CourseId;
            currentRegister.UserId = request.UserId;
            currentRegister.SubjectId = request.SubjectId;
            await _baseRegisterRepository.UpdateAsync(currentRegister);
            return new ResponseObject<DataResponseStudy>
            {
                Status = StatusCodes.Status200OK,
                Message = "Cập nhật đăng kí thành công!",
                Data = _studyConverter.EntityDTO(currentRegister)
            };
        }
        //LearningProcess
        public async Task<ResponseObject<DataResponseLearningProcess>> AddLearningProcess(Request_LearningProcess request)
        {
            var learningProcess = new LearningProgress
            {
                UserId = request.UserId,
                RegisterStudyId = request.RegisterStudyId,
                SubjectId = request.SubjectId,
            };
            await _baseLearningRepository.CreateAsync(learningProcess);
            return new ResponseObject<DataResponseLearningProcess>
            {
                Status = StatusCodes.Status200OK,
                Message = "Thêm tiến trình học thành công",
                Data = _learningConverter.EntityDTO(learningProcess)    
            };  
        }

        public async Task<ResponseObject<DataResponseLearningProcess>> UpdateLearningProcess(long learningProcessId, Request_LearningProcess request)
        {
            var currentLearning = await _baseLearningRepository.GetByIdAsync(learningProcessId);
            if (currentLearning == null)
            {
                return new ResponseObject<DataResponseLearningProcess>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tiến trình học không tồn tại",
                    Data = null
                };
            }
            currentLearning.UserId = request.UserId;
            currentLearning.RegisterStudyId = request.RegisterStudyId;  
            currentLearning.SubjectId = request.SubjectId;
            await _baseLearningRepository.UpdateAsync(currentLearning);
            return new ResponseObject<DataResponseLearningProcess>
            {
                Status = StatusCodes.Status200OK,
                Message = "Cập nhật tiến trình học thành công",
                Data = _learningConverter.EntityDTO(currentLearning)
            };
        }

        public async Task<ResponseObject<DataResponseLearningProcess>> DeleteLearningProcess(long learningProcessId)
        {
            var currentLearning = await _baseLearningRepository.GetByIdAsync(learningProcessId);
            if (currentLearning == null)
            {
                return new ResponseObject<DataResponseLearningProcess>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Tiến trình học không tồn tại",
                    Data = null
                };
            }
            await _baseLearningRepository.DeleteAsync(learningProcessId);
            return new ResponseObject<DataResponseLearningProcess>
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa tiến trình học thành công",
                Data = _learningConverter.EntityDTO(currentLearning)
            };
        }
    }
}
