using KhoaHocApi.Application.Handle.HandleFile;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.Mapper;
using KhoaHocApi.Application.Payloads.RequestModels.CertificateRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataCertificates;
using KhoaHocApi.Application.Payloads.ResponseModel.DataUsers;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.ImplementService
{
    public class CertificateService : ICertifiacateService
    {
        private readonly IBaseRepository<Certificate> _baseCertificateRepository;
        private readonly IBaseRepository<CertificateType> _baseCertificateTypeRepository;
        private readonly CertificateTypeConverter _certificateTypeConverter;
        private readonly CertificateConverter _certificateConverter;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly UserConverter _userConverter;

        public CertificateService(IBaseRepository<Certificate> baseCertificateRepository,
            IBaseRepository<CertificateType> baseCertificateTypeRepository,
            CertificateTypeConverter certificateTypeConverter,
            CertificateConverter certificateConverter,
            IHttpContextAccessor httpContextAccessor,
            IBaseRepository<User> baseUserRepository,
            IUserRepository userRepository,
            UserConverter userConverter)
        {
            _baseCertificateRepository = baseCertificateRepository;
            _baseCertificateTypeRepository = baseCertificateTypeRepository;
            _certificateTypeConverter = certificateTypeConverter;
            _certificateConverter = certificateConverter;
            _httpContextAccessor = httpContextAccessor;
            _baseUserRepository = baseUserRepository;
            _userRepository = userRepository;
            _userConverter = userConverter;
        }

        public async Task<IQueryable<CertificateType>> GetAllCertificateType()
        {
            var certificateTypeList = await _baseCertificateTypeRepository.GetAllAsync();
            return certificateTypeList ?? Enumerable.Empty<CertificateType>().AsQueryable();
        }

        public async Task<IQueryable<DataResponseUser>> GetUserHaveCert()
        {
            var listUser = await _userRepository.GetUserHaveCertificate();
            var listResponseUser = listUser.Select(x => _userConverter.EntityToDTO(x));
            return listResponseUser  ?? Enumerable.Empty<DataResponseUser>().AsQueryable();
        }

        public async Task<IQueryable<Certificate>> GetAllCertificate()
        {
            var certificateList = await _baseCertificateRepository.GetAllAsync();
            return certificateList ?? Enumerable.Empty<Certificate>().AsQueryable();
        }

        public async Task<IQueryable<Certificate>> GetCertificateById(long id)
        {
            var certificateListById = await _baseCertificateRepository.GetAllAsync(c => c.CertificateTypeId == id);
            return certificateListById ?? Enumerable.Empty<Certificate>().AsQueryable();
        }

        public async Task<ResponseObject<DataResponseCertificate>> AddCertificate(Request_Certificate request)
        {

            var user = _httpContextAccessor.HttpContext.User;
            try
            {
                if(!user.Identity.IsAuthenticated)
                {
                    return new ResponseObject<DataResponseCertificate>()
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Message = "Người dùng chưa được xác thực",
                        Data = null,
                    };
                }

                var checkType = await _baseCertificateTypeRepository.GetByIdAsync(u => u.Id == request.CertificateTypeId);
                if (checkType == null)
                {
                    return new ResponseObject<DataResponseCertificate>()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Loại chứng chỉ này không tồn tại! Hãy thêm mới!",
                        Data = null
                    };
                }
                var checkName = await _baseCertificateRepository.GetAsync(x => x.Name == request.Name);
                if (checkName != null)
                {
                    return new ResponseObject<DataResponseCertificate>()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Bạn đã có chứng chỉ này!",
                        Data = null
                    };
                }

                Certificate certificate = new Certificate();
                certificate.Name = request.Name;
                certificate.Description = request.Description;
                certificate.Image = await HandleUploadFile.Upfile(request.Image);
                certificate.CertificateTypeId = request.CertificateTypeId;
                var cer = await _baseCertificateRepository.CreateAsync(certificate);

                var currentUsername = user.FindFirst("Username").Value;
                var currentUser = await _baseUserRepository.GetAsync(x => x.Username == currentUsername);

                currentUser.CertificateId = cer.Id;
                await _baseUserRepository.UpdateAsync(currentUser);

                return new ResponseObject<DataResponseCertificate>()
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thêm chứng chỉ thành công!",
                    Data = _certificateConverter.EntityDTO(certificate)
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseCertificate>()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<ResponseObject<DataResponseCertificate>> UpdateCertificate(long id, Request_Certificate request)
        {
            var currentCertificate = await _baseCertificateRepository.GetByIdAsync(id);
            if (currentCertificate != null)
            {
                return new ResponseObject<DataResponseCertificate>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chứng chỉ này đã tồn tại!",
                    Data = null
                };
            }
            currentCertificate.Name = request.Name;
            currentCertificate.Description = request.Description;
            currentCertificate.Image = await HandleUploadFile.Upfile(request.Image);
            currentCertificate.CertificateTypeId = request.CertificateTypeId;

            await _baseCertificateRepository.UpdateAsync(currentCertificate);
            return new ResponseObject<DataResponseCertificate>
            {
                Status = StatusCodes.Status200OK,
                Message = "Cập nhật chứng chỉ thành công!",
                Data = _certificateConverter.EntityDTO(currentCertificate)
            };
        }
        public async Task<ResponseObject<DataResponseCertificate>> DeleteCertificate(long id)
        {
            var currentCertificate = await _baseCertificateRepository.GetByIdAsync(id);
            if (currentCertificate == null)
            {
                return new ResponseObject<DataResponseCertificate>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Loại chứng chỉ này không tồn tại!",
                    Data = null
                };
            }
            await _baseCertificateTypeRepository.DeleteAsync(id);
            return new ResponseObject<DataResponseCertificate>
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa loại chứng chỉ thành công",
                Data = _certificateConverter.EntityDTO(currentCertificate)
            };
        }
        //Certificate Type
        public async Task<ResponseObject<DataResponseCertificateType>> AddCertificateType(Request_CertificateType request)
        {
            var checkType = await _baseCertificateTypeRepository.GetAsync(u => u.Name == request.Name);
            if (checkType != null)
            {
                return new ResponseObject<DataResponseCertificateType>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Loại chứng chỉ này đã tồn tại!",
                    Data = null
                };
            }
            else
            {
                CertificateType certificateType = new CertificateType();
                certificateType.Name = request.Name;
                await _baseCertificateTypeRepository.CreateAsync(certificateType);
                return new ResponseObject<DataResponseCertificateType>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Thêm loại chứng chỉ thành công!",
                    Data = _certificateTypeConverter.EntityDTO(certificateType)
                };
            }
        }
        public async Task<ResponseObject<DataResponseCertificateType>> UpdateCertificateType(long id, Request_CertificateType request)
        {
            var currentType = await _baseCertificateTypeRepository.GetByIdAsync(id);
            if (currentType == null)
            {
                return new ResponseObject<DataResponseCertificateType>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Loại chứng chỉ này không tồn tại!",
                    Data = null
                };
            }
            currentType.Name = request.Name ?? string.Empty;
            await _baseCertificateTypeRepository.UpdateAsync(currentType);
            return new ResponseObject<DataResponseCertificateType>
            {
                Status = StatusCodes.Status200OK,
                Message = "Thay đổi chứng chỉ thành công!",
                Data = _certificateTypeConverter.EntityDTO(currentType)
            };
        }
        public async Task<ResponseObject<DataResponseCertificateType>> DeleteCertificateType(long id)
        {
            var currentType = await _baseCertificateTypeRepository.GetByIdAsync(id);
            if (currentType == null)
            {
                return new ResponseObject<DataResponseCertificateType>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Loại chứng chỉ này không tồn tại!",
                    Data = null
                };
            }
            await _baseCertificateTypeRepository.DeleteAsync(id);
            return new ResponseObject<DataResponseCertificateType>
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa loại chứng chỉ thành công",
                Data = _certificateTypeConverter.EntityDTO(currentType)
            };
        }


    }
}
