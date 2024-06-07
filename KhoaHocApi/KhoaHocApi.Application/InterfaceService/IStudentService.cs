using BaiTestPost.Handler.Pagination;
using KhoaHocApi.Application.Payloads.RequestModels.InputRequest;
using KhoaHocApi.Application.Payloads.RequestModels.StudentsRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataStudents;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.InterfaceService
{
    public interface IStudentService
    {
        Task<ResponseObject<DataResponseBlog>> CreateBlog(Request_CreateLog request);
        Task<ResponseObject<DataResponseBlog>> UpdateBlog(long IdBlog,Request_CreateLog request);
        Task<ResponseObject<DataResponseBlog>> DeleteBlog(long IdBlog);
        Task<PageResult<DataResponseBlog>> GetAlls(FilterBlog input, int pageNumber, int pageSize);
        Task<ResponseObject<DataResponseBlog>> GetBlogById(long IdBlog);

        Task<ResponseObject<DataResponseLikeBlog>> LikeOrUnlike(long IdBlog);

        Task<ResponseObject<DataResponseComment>> CommentBlog(long IdBlog, string content);
        Task<ResponseObject<DataResponseComment>> UpdateComment(long Idcomment, string content);
        Task<ResponseObject<DataResponseComment>> DeleteComment(long Idcomment);
        Task<PageResult<DataResponseComment>> GetAllComment(long IdBlog,int pageNumber, int pageSize);

        Task<ResponseObject<DataResponseMakeQuestion>> CreateMakeQuest(long subjectdetailId, string question);
        Task<ResponseObject<DataResponseMakeQuestion>> UpdateQuestion(long questionId, string question);
        Task<ResponseObject<DataResponseMakeQuestion>> DeleteQuestion(long questionId);
        Task<PageResult<DataResponseMakeQuestion>> GetAllQuestion(long IdsubjectDetail, int pageNumber, int pageSize);
        Task<IQueryable<DataResponseMakeQuestion>> GetAllQuestionByCourseId(long courseId);
        Task<ResponseObject<DataResponseMakeQuestion>> GetQuestionById(long questionId);

        Task<ResponseObject<DataResponseAnswer>> CreateAnswer(long questionid, string answer);
        Task<ResponseObject<DataResponseAnswer>> UpdateAnswer(long answerid, string answer);
        Task<ResponseObject<DataResponseAnswer>> DeleteAnswer(long answerid);
        Task<PageResult<DataResponseAnswer>> GetAlAnswer(long questionId, int pageNumber, int pageSize);
        

    }
}
