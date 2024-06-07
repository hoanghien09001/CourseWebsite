using KhoaHocApi.Application.ImplementService;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.RequestModels.CourseRequest;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoaHocApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet("search-course")]
        public async Task<IActionResult> SearchCourse(string? courseName = null, string? code = null, double? minPrice = null, double? maxPrice = null)
        {
            return Ok(await _courseService.SearchCourse(courseName, code, minPrice, maxPrice));
        }
        [HttpGet("get-course")]
        public async Task<IActionResult> GetCourseById( long id)
        {
            return Ok(await _courseService.GetCourseById(id));
        }
        [HttpPost("add-course")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddCourse([FromForm] Request_Course request)
        {
            long id = long.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _courseService.AddCourse(id,request));
        }
        [HttpPost("update-course/{id}")]
        public async Task<IActionResult> UpdateCourse([FromRoute] long id, [FromForm] Request_Course request)
        {
            return Ok(await _courseService.UpdateCourse(id, request));
        }
        [HttpDelete("delete-course/{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] long id)
        {
            return Ok(await _courseService.DeleteCourse(id));
        }
        [HttpGet("get-all-course-by-teacher")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllCourseByTeacher()
        {
            return Ok(await _courseService.GetAllCourseByTeacher());
        }
    }
}