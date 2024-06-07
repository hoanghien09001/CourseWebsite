using BaiTestPost.Handler.Pagination;
using CloudinaryDotNet.Actions;
using KhoaHocApi.Application.Payloads.RequestModels.SubjectRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataSubject;
using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.InterfaceService
{
    public interface ISubjectService
    {
        //Subject
        Task<ResponseObject<DataResponseSubject>> GetSubjectById(long id);
        Task<IQueryable<DataResponseSubject>> GetAllSubject(long? courseId);
        Task<ResponseObject<DataResponseSubject>> AddSubject(Request_Subject request);
        Task<ResponseObject<DataResponseCourseSubject>> AddSubjectInCourse(long courseId, Request_Subject request);
        Task<ResponseObject<DataResponseSubject>> UpdateSubject(long id,Request_Subject request);
        Task<ResponseObject<DataResponseSubject>> DeleteSubject(long id);
        //SubjectDetails
        Task<IQueryable<DataResponseSubjectDetails>> GetAllSubjectDetails(long subjectId);
        Task<ResponseObject<DataResponseSubjectDetails>> AddSubjectDetails(Request_SubjectDetails request);
        Task<ResponseObject<DataResponseSubjectDetails>> UpdateSubjectDetails(long subjectDetailId, Request_SubjectDetails request);
        Task<ResponseObject<DataResponseSubjectDetails>> DeleteSubjectDetails(long subjectDetailsId);
    }
}
