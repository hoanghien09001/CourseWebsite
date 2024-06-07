using BaiTestPost.Handler.Pagination;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.Mapper;
using KhoaHocApi.Application.Payloads.Mapper.BillsConverter;
using KhoaHocApi.Application.Payloads.Mapper.PracticeConverter;
using KhoaHocApi.Application.Payloads.Mapper.StudentConverter;
using KhoaHocApi.Application.Payloads.RequestModels.DohomeworkRequest;
using KhoaHocApi.Application.Payloads.RequestModels.InputRequest;
using KhoaHocApi.Application.Payloads.RequestModels.PracticeRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataBill;
using KhoaHocApi.Application.Payloads.ResponseModel.DataDohomework;
using KhoaHocApi.Application.Payloads.ResponseModel.DataPractice;
using KhoaHocApi.Application.Payloads.ResponseModel.DataStudents;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.Enumerates;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.ImplementRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.ImplementService
{
    public class PracticeService : IPractiveService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<SubjectDetail> _subjectdetailRepository;
        private readonly IBaseRepository<Practice> _practiceRepository;
        private readonly PractiveConverter _practiveConverter;
        private readonly IBaseRepository<ProgramingLanguage> _languagerepository;
        private readonly IBaseRepository<RegisterStudy> _registerRepository;
        private readonly DohomeworkConverter _dohomeworkConverter;
        private readonly IBaseRepository<DoHomework>_dohomeRepository;
        public PracticeService(IHttpContextAccessor httpContext, IBaseRepository<User> userRepository, IBaseRepository<SubjectDetail> subjectRepository
            , IBaseRepository<Practice> practiceRepository, PractiveConverter practiveConverter, IBaseRepository<ProgramingLanguage> languagerepository
            , IBaseRepository<RegisterStudy> registerRepository, DohomeworkConverter dohomeworkConverter, IBaseRepository<DoHomework> dohomeRepository)
        {
            _httpContext = httpContext;
            _userRepository = userRepository;
            _subjectdetailRepository = subjectRepository;
            _practiceRepository = practiceRepository;
            _practiveConverter = practiveConverter;
            _languagerepository = languagerepository;
            _registerRepository = registerRepository;
            _dohomeworkConverter = dohomeworkConverter;
            _dohomeRepository = dohomeRepository;
        }

        #region//CreatePractice
        public async Task<ResponseObject<DataResponsePractice>> CreatePractice(Request_Practicee request)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }
            if (currentUser.IsInRole("User"))
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Bạn không có quyền hạn!",
                    Data = null,
                };
            }
            var subjectDetail = await _subjectdetailRepository.GetByIdAsync(x => x.Id == request.SubjectDetailId && x.IsActive == true);
            if (subjectDetail == null)
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "subjectdetail không tồn tại!",
                    Data = null,
                };
            }
            var langauge = await _languagerepository.GetByIdAsync(x => x.Id == request.ProgramingLannguageId);
            if (langauge == null)
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Programng Langauge  không tồn tại!",
                    Data = null,
                };
            }
            Practice practice = new Practice()
            {
                CreateTime = DateTime.Now,
                ExpectOutput = request.ExpectOutput,
                Level = request.Level,
                IsRequired = request.isRequired,
                SubjectDetailId = subjectDetail.Id,
                Title = request.Title,
                Topic = request.Topic,
                ProgramingLannguageId = request.ProgramingLannguageId,
                PracticeCode = request.PracticeCode,
                IsDeleted = false,
                MediumScore = 0,
            };
            await _practiceRepository.CreateAsync(practice);
            return new ResponseObject<DataResponsePractice>
            {
                Status = StatusCodes.Status200OK,
                Message = "Thêm practice thành công!",
                Data = _practiveConverter.EntityDTO(practice),
            };
        }

        #endregion

        #region//UpdatePractice
        public async Task<ResponseObject<DataResponsePractice>> UpdatePractice(long practiceid, Request_UpdatePractice request)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }
            if (currentUser.IsInRole("User"))
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Bạn không có quyền hạn!",
                    Data = null,
                };
            }
            var practice = await _practiceRepository.GetByIdAsync(x => x.Id == practiceid);
            if (practice == null || practice.IsDeleted == true)
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Practive không tồn tại",
                    Data = null,
                };
            }
            practice.UpdateTime = DateTime.Now;
            practice.ExpectOutput = request.ExpectOutput;
            practice.Level = request.Level;
            practice.IsRequired = request.isRequired;
            practice.Title = request.Title;
            practice.Topic = request.Topic;
            practice.ProgramingLannguageId = request.ProgramingLannguageId;
            practice.PracticeCode = request.PracticeCode;

            await _practiceRepository.UpdateAsync(practice);
            return new ResponseObject<DataResponsePractice>
            {
                Status = StatusCodes.Status200OK,
                Message = "Update practice thành công!",
                Data = _practiveConverter.EntityDTO(practice),
            };

        }

        #endregion

        #region//DeletePractice
        public async Task<ResponseObject<DataResponsePractice>> DeletePractice(long practiceid)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }
            if (currentUser.IsInRole("User"))
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Bạn không có quyền hạn!",
                    Data = null,
                };
            }
            var practice = await _practiceRepository.GetByIdAsync(x => x.Id == practiceid);
            if (practice == null || practice.IsDeleted == true)
            {
                return new ResponseObject<DataResponsePractice>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Practive không tồn tại",
                    Data = null,
                };
            }
            practice.IsDeleted = true;
            await _practiceRepository.UpdateAsync(practice);
            return new ResponseObject<DataResponsePractice>
            {
                Status = StatusCodes.Status200OK,
                Message = "Delete practice thành công!",
                Data = _practiveConverter.EntityDTO(practice),
            };
        }
        #endregion

        #region//getAllPractice
        public async Task<PageResult<DataResponsePractice>> GetAllPractice(FilterPractice practice, int pageNumber, int pageSize)
        {
            var list = await _practiceRepository.GetAllAsync(x => x.IsDeleted == false);

            if (practice.SubjectDetailId.HasValue)
            {
                list = list.Where(x => x.SubjectDetailId == practice.SubjectDetailId);
            }
            if (practice.ProgramingLannguageId.HasValue)
            {
                list = list.Where(x => x.ProgramingLannguageId == practice.ProgramingLannguageId);
            }
            if (practice.Level.HasValue)
            {
                list = list.Where(x => x.Level == practice.Level);
            }

            if (!string.IsNullOrWhiteSpace(practice.PracticeCode))
            {
                list = list.Where(x => x.PracticeCode != null && x.PracticeCode.ToLower().Contains(practice.PracticeCode.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(practice.Topic))
            {
                list = list.Where(x => x.Topic != null && x.Topic.ToLower().Contains(practice.Topic.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(practice.Title))
            {
                list = list.Where(x => x.Title != null && x.Title.ToLower().Contains(practice.Title.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(practice.ExpectOutput))
            {
                list = list.Where(x => x.ExpectOutput != null && x.ExpectOutput.ToLower().Contains(practice.ExpectOutput.ToLower()));
            }

            var query = list.ToList().Select(item =>
            {
                try
                {
                    return _practiveConverter.EntityDTO(item);
                }
                catch (NullReferenceException ex)
                {
                    return null; //
                }
            }).Where(dto => dto != null);

            var result = Pagination.GetPagedData(query.AsQueryable(), pageNumber, pageSize);
            return result;
        }

        #endregion

        #region//GetPracticeById
        public async Task<ResponseObject<DataResponsePractice>> GetPracticeById(long practiceid)
        {
            var list = await _practiceRepository.GetByIdAsync(x => x.Id == practiceid && x.IsDeleted == false);
            var dta = _practiveConverter.EntityDTO(list);
            return new ResponseObject<DataResponsePractice>
            {
                Status = StatusCodes.Status200OK,
                Message = "hiển thị practice thành công!",
                Data = dta,
            };

        }

        #endregion
        public async Task<ResponseObject<DataResponseDohomework>> Dohomework(Request_Dohomework request)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseDohomework>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseDohomework>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }
            /*if (currentUser.IsInRole("User"))
            {
                return new ResponseObject<DataResponseDohomework>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Bạn không có quyền hạn!",
                    Data = null,
                };
            }*/
            var practices =await _practiceRepository.GetByIdAsync(x => x.Id == request.PracticeId && x.IsDeleted == false);
            if (practices == null)
            {
                return new ResponseObject<DataResponseDohomework>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Không tồn tại practice!",
                    Data = null,
                };
            }
            
            var resgisterstudy =await _registerRepository.GetByIdAsync(x => x.Id == request.RegisterStudyId && x.IsActive == true && x.IsFinished == false);
            if (resgisterstudy == null)
            {
                return new ResponseObject<DataResponseDohomework>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Bạn chưa đăng ký học!",
                    Data = null,
                };
            }
            
            DoHomework doHomework = new DoHomework()
            {
                UserId = idUser,
                PracticeId = practices.Id,
                RegisterStudyId = resgisterstudy.Id,
                ActualOutput = request.ActualOutput,
                DoneTime = DateTime.Now,
                HomeworkStatus = ConstantEnum.HomeworkEnum.Finished,
                IsFinished = true,
            };
            await _dohomeRepository.CreateAsync(doHomework);
            return new ResponseObject<DataResponseDohomework>
            {
                Status = StatusCodes.Status200OK,
                Message = "Làm bài tập thành công!",
                Data = _dohomeworkConverter.EntityDTO(doHomework),
            };
        }
    }
}

