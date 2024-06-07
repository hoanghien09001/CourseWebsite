using BaiTestPost.Handler.Pagination;
using KhoaHocApi.Application.Constant;
using KhoaHocApi.Application.ImplementService;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.RequestModels.InputRequest;
using KhoaHocApi.Application.Payloads.RequestModels.StudentsRequest;
using KhoaHocApi.Application.Payloads.ResponseModel.DataStudents;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoaHocApi.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        //CreateBlog
        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] Request_CreateLog request)
        {
            return Ok(await _studentService.CreateBlog(request));
        }
        //UpdateBlog
        [HttpPut]
        public async Task<IActionResult> UpdateBlog(long IdBlog, [FromBody] Request_CreateLog request)
        {
            return Ok(await _studentService.UpdateBlog(IdBlog,request));
        }
        //DeleteBlog
        [HttpDelete]
        public async Task<IActionResult> DeleteBlog(long IdBlog)
        {
            return Ok(await _studentService.DeleteBlog(IdBlog));
        }

        //GetAllBlog
        [HttpGet]
        public async Task<IActionResult>GetAllBlog([FromQuery]FilterBlog? Input, int pageNumber=1, int pageSize=10)
        {
            return Ok(await _studentService.GetAlls(Input, pageNumber, pageSize));
        }

        //GetBlogById
        [HttpGet]
        public async Task<IActionResult> GetBlogById(long IdBlog)
        {
            return Ok(await _studentService.GetBlogById(IdBlog));
        }

        //LikeBlog
        [HttpPost]
        public async Task<IActionResult> LikeOrUnlikeBlog(long IdBlog)
        {
            return Ok(await _studentService.LikeOrUnlike(IdBlog));
        }

        //CommentBlog
        [HttpPost]
        public async Task<IActionResult> CommentBlog(long IdBlog, string content)
        {
            return Ok(await _studentService.CommentBlog(IdBlog,content));
        }

        //UpdateCommentBlog
        [HttpPut]
        public async Task<IActionResult> UpdateComment(long IdComment, string content)
        {
            return Ok(await _studentService.UpdateComment(IdComment, content));
        }

        //DeleteComment
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(long IdComment)
        {
            return Ok(await _studentService.DeleteComment(IdComment));
        }

        //GetAllComment in BlogId
        [HttpGet]
        public async Task<IActionResult> GetAllComment(long IdBlog,int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _studentService.GetAllComment(IdBlog,pageNumber, pageSize));
        }
        //MakeQuestion
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateMakeQuestion(long SubjectDetailId, string Question)
        {
            return Ok(await _studentService.CreateMakeQuest(SubjectDetailId, Question));
        }

        //UpdateMakeQuestion
        [HttpPut]
        public async Task<IActionResult> UpdateQuestion(long QuestionId, string Question)
        {
            return Ok(await _studentService.UpdateQuestion(QuestionId, Question));
        }


        //DeleteMakeQuestion
        [HttpDelete]
        public async Task<IActionResult> DeleteQuestion(long QuestionId)
        {
            return Ok(await _studentService.DeleteQuestion(QuestionId));
        }

        //GetAllQuestion In SubjectDetailId
        [HttpGet]
        public async Task<IActionResult> GetAllQuestion(long IdsubjectDetail, int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _studentService.GetAllQuestion(IdsubjectDetail, pageNumber, pageSize));
        }
        //GetAllQuestionByCourseId
        [HttpGet]
        public async Task<IActionResult> GetAllQuestionByCourseId(long courseId)
        {
            return Ok(await _studentService.GetAllQuestionByCourseId(courseId));
        }

        //GetQuestionById
        [HttpGet]
        public async Task<IActionResult> GetQuestionById(long QuestionId)
        {
            return Ok(await _studentService.GetQuestionById(QuestionId));
        }

        //CreateAnswer
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(long QuestionId, string Answer)
        {
            return Ok(await _studentService.CreateAnswer(QuestionId, Answer));
        }

        //UpdateAnswer
        [HttpPut]
        public async Task<IActionResult> UpdateAnswer(long AnswerId, string Answer)
        {
            return Ok(await _studentService.UpdateAnswer(AnswerId, Answer));
        }

        //Delete Answer
        [HttpDelete]
        public async Task<IActionResult> DeleteAnswer(long AnswerId)
        {
            return Ok(await _studentService.DeleteAnswer(AnswerId));
        }

        //GetAllAnswerInQuestionId
        [HttpGet]
        public async Task<IActionResult> GetAllAnswer(long QuestionId, int pageNumber = 1, int pageSize = 10)
        {
            return Ok(await _studentService.GetAlAnswer(QuestionId, pageNumber, pageSize));
        }
    }
}
