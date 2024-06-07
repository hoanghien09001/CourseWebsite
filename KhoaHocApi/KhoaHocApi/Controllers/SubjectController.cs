using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.RequestModels.SubjectRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoaHocApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        //Subject
        [HttpGet("get-all-subject/{courseId}")]
        public async Task<IActionResult> GetAllSubject(long courseId)
        {
            return Ok(await _subjectService.GetAllSubject(courseId));
        }
        [HttpGet("get-subject/{subjectId}")]
        public async Task<IActionResult> GetSubjectById([FromRoute] long subjectId)
        {
            return Ok(await _subjectService.GetSubjectById(subjectId));
        }
        [HttpPost("add-subject")]
        public async Task<IActionResult> AddSubject([FromBody] Request_Subject request)
        {
            return Ok(await _subjectService.AddSubject(request));
        }
        [HttpPost("add-subject-in-course")]
        public async Task<IActionResult> AddSubjectInCourse([FromQuery] long courseId, [FromBody] Request_Subject request)
        {
            return Ok(await _subjectService.AddSubjectInCourse(courseId, request));
        }
        [HttpPut("update-subject/{subjectId}")]
        public async Task<IActionResult> UpdateSubject([FromRoute] long subjectId, [FromForm] Request_Subject request)
        {
            return Ok(await _subjectService.UpdateSubject(subjectId, request));
        }
        [HttpDelete("delete-subject/{subjectId}")]
        public async Task<IActionResult> DeleteSubject([FromRoute] long subjectId)
        {
            return Ok(await _subjectService.DeleteSubject(subjectId));
        }
        //SubjectDetail
        [HttpGet("get-all-subjectdetails/{subjectId}")]
        public async Task<IActionResult> GetAllSubjectDetails(long subjectId)
        {
            return Ok(await _subjectService.GetAllSubjectDetails(subjectId));
        }
        [HttpPost("add-subjectdetail")]
        public async Task<IActionResult> AddSubjectDetail([FromForm] Request_SubjectDetails request)
        {
            return Ok(await _subjectService.AddSubjectDetails(request));
        }
        [HttpPut("update-subjectdetail/{subjectDetailId}")]
        public async Task<IActionResult> UpdateSubjectDetail([FromRoute] long subjectDetailId, [FromForm] Request_SubjectDetails request)
        {
            return Ok(await _subjectService.UpdateSubjectDetails(subjectDetailId, request));
        }
        [HttpDelete("delete-subjectdetail/{subjectDetailId}")]
        public async Task<IActionResult> DeleteSubjectDetail([FromRoute] long subjectDetailId)
        {
            return Ok(await _subjectService.DeleteSubjectDetails(subjectDetailId));
        }
    }
}
