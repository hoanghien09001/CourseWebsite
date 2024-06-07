using BaiTestPost.Handler.Pagination;
using KhoaHocApi.Application.Handle.HandleFile;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.Mapper.SubjectConverter;
using KhoaHocApi.Application.Payloads.RequestModels.SubjectRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataSubject;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.ImplementService
{
    public class SubjectService : ISubjectService
    {
        private readonly IBaseRepository<Subject> _baseSubjectRepository;
        private readonly IBaseRepository<SubjectDetail> _baseSubjectDetaiRepository;
        private readonly IBaseRepository<Course> _baseCourseRepository;
        private readonly IBaseRepository<CourseSubject> _baseCourseSubjectRepository;
        private readonly SubjectConverter _subjectConverter;
        private readonly SubjectDetailsConverter _subjectDetailsConverter;
        private readonly CourseSubjectConverter _courseSubjectConverter;
        
        public SubjectService(IBaseRepository<Subject> baseSubjectRepository, IBaseRepository<SubjectDetail> baseSubjectDetaiRepository, 
                              SubjectConverter subjectConverter, SubjectDetailsConverter subjectDetailsConverter, 
                              IBaseRepository<Course> baseCourseRepository, IBaseRepository<CourseSubject> baseCourseSubjectRepository, 
                              CourseSubjectConverter courseSubjectConverter)
        {
            _baseSubjectRepository = baseSubjectRepository;
            _baseSubjectDetaiRepository = baseSubjectDetaiRepository;
            _subjectConverter = subjectConverter;
            _subjectDetailsConverter = subjectDetailsConverter;
            _baseCourseRepository = baseCourseRepository;
            _baseCourseSubjectRepository = baseCourseSubjectRepository;
            _courseSubjectConverter = courseSubjectConverter;
        }
        //Subject
        public async Task<ResponseObject<DataResponseSubject>> GetSubjectById(long id)
        {
            var subject = await _baseSubjectRepository.GetByIdAsync(id);
            if(subject == null)
            {
                return new ResponseObject<DataResponseSubject>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Môn học không tồn tại",
                    Data = null
                };
            }
            return new ResponseObject<DataResponseSubject>
            {
                Status = StatusCodes.Status200OK,
                Message = "Môn học cần tìm",
                Data = _subjectConverter.EntityDTO(subject)
            };
        }
        public async Task<IQueryable<DataResponseSubject>> GetAllSubject(long? courseId)
        {
            if(courseId == null)
            {
                var list = await _baseSubjectRepository.GetAllAsync();
                var query = list.ToList().Select(item => _subjectConverter.EntityDTO(item));
                var result = query.AsQueryable();
                return result;
            }
            else
            {
                var listSubjectInCourse = await _baseCourseSubjectRepository.GetAllAsync(x => x.CourseId == courseId);
                var subjectIds = listSubjectInCourse.Select(x => x.SubjectId).ToList();
                var subjects = await _baseSubjectRepository.GetAllAsync(x => subjectIds.Contains(x.Id));
                var query = subjects.Select(item => _subjectConverter.EntityDTO(item));
                var result = query.AsQueryable();
                return result;
            }
        }
        public async Task<ResponseObject<DataResponseSubject>> AddSubject(Request_Subject request)
        {
            var subject = new Subject
            {
                SubjectName = request.SubjectName,
                Symbol = request.Symbol,
                IsActive = false
            };
            await _baseSubjectRepository.CreateAsync(subject);
            subject.IsActive = true;
            await _baseSubjectRepository.UpdateAsync(subject);
            return new ResponseObject<DataResponseSubject>
            {
                Status = StatusCodes.Status200OK,
                Message = "Thêm môn học thành công!",
                Data = _subjectConverter.EntityDTO(subject)
            };
        }
        public async Task<ResponseObject<DataResponseCourseSubject>> AddSubjectInCourse(long courseId, Request_Subject request)
        {
            var subject = new Subject
            {
                SubjectName = request.SubjectName,
                Symbol = request.Symbol,
                IsActive = false
            };
            await _baseSubjectRepository.CreateAsync(subject);
            subject.IsActive = true;
            await _baseSubjectRepository.UpdateAsync(subject);

            var course = await _baseCourseRepository.GetByIdAsync(courseId);
            if(course == null)
            {
                return new ResponseObject<DataResponseCourseSubject>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Khóa học không tồn tại",
                    Data = null
                };
            }
            var courseSubject = new CourseSubject
            {
                CourseId = courseId,
                SubjectId = subject.Id,
            };
            await _baseCourseSubjectRepository.CreateAsync(courseSubject);
            return new ResponseObject<DataResponseCourseSubject>
            {
                Status = StatusCodes.Status200OK,
                Message = "Thêm môn học vào khóa học thành công!",
                Data = _courseSubjectConverter.EntityDTO(courseSubject)
            };
        }
        public async Task<ResponseObject<DataResponseSubject>> UpdateSubject(long id, Request_Subject request)
        {
            var subject = await _baseSubjectRepository.GetByIdAsync(id);
            if(subject == null)
            {
                return new ResponseObject<DataResponseSubject>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Môn học không tồn tại",
                    Data = null
                };
            }
            subject.SubjectName = request.SubjectName;
            subject.Symbol = request.Symbol;
            await _baseSubjectRepository.UpdateAsync(subject);
            return new ResponseObject<DataResponseSubject>
            {
                Status = StatusCodes.Status200OK,
                Message = "Cập nhật môn học thành công!",
                Data = _subjectConverter.EntityDTO(subject)
            };

        }       
        public async Task<ResponseObject<DataResponseSubject>> DeleteSubject(long id)
        {
            var subject = await _baseSubjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return new ResponseObject<DataResponseSubject>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Môn học không tồn tại",
                    Data = null
                };
            }
            await _baseSubjectRepository.DeleteAsync(id);
            return new ResponseObject<DataResponseSubject>
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa môn học thành công!",
                Data = _subjectConverter.EntityDTO(subject)
            };
        }
        
        //SubjectDetails
        public async Task<IQueryable<DataResponseSubjectDetails>> GetAllSubjectDetails(long subjectId)
        {      
            var listSubjectDetail = await _baseSubjectDetaiRepository.GetAllAsync(x => x.SubjectId == subjectId);
            var query = listSubjectDetail.ToList().Select(item => _subjectDetailsConverter.EntityDTO(item));
            var result = query.AsQueryable();
            return result;            
        }
        public async Task<ResponseObject<DataResponseSubjectDetails>> AddSubjectDetails(Request_SubjectDetails request)
        {
            var subject = await _baseSubjectRepository.GetByIdAsync(request.SubjectId);
            if(subject == null)
            {
                return new ResponseObject<DataResponseSubjectDetails>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Môn học không tồn tại!",
                    Data = null
                };
            }
            var subjectDetail = new SubjectDetail
            {
                SubjectDetailName = request.SubjectDetailName,
                LinkVideo = request.LinkVideo,
                SubjectId = request.SubjectId,
                IsFinished = false,
                IsActive = false
            };
            await _baseSubjectDetaiRepository.CreateAsync(subjectDetail);
            subjectDetail.IsActive = true;
            await _baseSubjectDetaiRepository.UpdateAsync(subjectDetail);
            return new ResponseObject<DataResponseSubjectDetails>
            {
                Status = StatusCodes.Status200OK,
                Message = "Thêm chi tiết môn học thành công!",
                Data = _subjectDetailsConverter.EntityDTO(subjectDetail)
            };
        }              
        public async Task<ResponseObject<DataResponseSubjectDetails>> DeleteSubjectDetails(long subjectDetailsId)
        {
            var subjectDetail = await _baseSubjectDetaiRepository.GetByIdAsync(subjectDetailsId);
            if(subjectDetail == null)
            {
                return new ResponseObject<DataResponseSubjectDetails>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Không tồn tại chi tiết này!",
                    Data = null
                };
            }
            await _baseSubjectDetaiRepository.DeleteAsync(subjectDetailsId);
            return new ResponseObject<DataResponseSubjectDetails>
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa chi tiết thành công!",
                Data = _subjectDetailsConverter.EntityDTO(subjectDetail)
            };
        }
        public async Task<ResponseObject<DataResponseSubjectDetails>> UpdateSubjectDetails(long subjectDetailId, Request_SubjectDetails request)
        {
            var subjectDetail = await _baseSubjectDetaiRepository.GetByIdAsync(subjectDetailId);
            if(subjectDetail == null)
            {
                return new ResponseObject<DataResponseSubjectDetails>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Không tồn tại chi tiết này!",
                    Data = null
                };
            }
            subjectDetail.SubjectDetailName = request.SubjectDetailName;
            subjectDetail.LinkVideo = request.LinkVideo;
            subjectDetail.SubjectId = request.SubjectId;
            await _baseSubjectDetaiRepository.UpdateAsync(subjectDetail);
            return new ResponseObject<DataResponseSubjectDetails>
            {
                Status = StatusCodes.Status200OK,
                Message = "Cập nhật chi tiết thành công",
                Data = _subjectDetailsConverter.EntityDTO(subjectDetail)
            };
        }
    }
}
