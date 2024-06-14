using KhoaHocApi.Application.Payloads.RequestModels.CourseRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataCourse;
using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.InterfaceService
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> SearchCourse(string? courseName, string? code, double? minPrice, double? maxPrice);
        Task<ResponseObject<DataResponseCourse>> GetCourseById(long id);
        Task<ResponseObject<DataResponseCourse>> AddCourse(long userId,Request_Course request);
        Task<ResponseObject<DataResponseCourse>> UpdateCourse(long id, Request_Course request);
        Task<ResponseObject<DataResponseCourse>> DeleteCourse(long id);
        Task<ResponseObject<IEnumerable<DataResponseCourse>>> GetAllCourseByTeacher();
        Task<ResponseObject<IEnumerable<DataResponseCourse>>> GetAllCourse();
    }
}