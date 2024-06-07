using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.RequestModels.StudyRequest;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoaHocApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyController : ControllerBase
    {
        private readonly IStudyService _studyService;
        public StudyController(IStudyService studyService)
        {
            _studyService = studyService;
        }
        //RegisterStudy
        [HttpGet("get-all-register")]
        public async Task<IActionResult> GetAllRegisterStudy()
        {
            return Ok(await _studyService.GetAllRegisterStudy());
        }
        [HttpGet("get-register-by-id")]
        public async Task<IActionResult> GetRegisterById([FromRoute] long registerId)
        {
            return Ok(await _studyService.GetRegisterStudyById(registerId));
        }
        [HttpPost("add-register")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddRegisterStudy(Request_Study request)
        {
            long id = long.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _studyService.AddRegisterSudy(id,request));
        }
        [HttpPut("update-register/{registerId}")]
        public async Task<IActionResult> UpdateRegisterStudy([FromRoute] long registerId, Request_Study request)
        {
            return Ok(await _studyService.UpdateRegisterStudy(registerId, request));
        }
        [HttpDelete("delete-register")]
        public async Task<IActionResult> DeleteRegisterStudy([FromRoute] long registerId)
        {
            return Ok(await _studyService.DeleteRegisterStudy(registerId)); 
        }
        //LearningProcess
        [HttpPost("add-learningprocess")]
        public async Task<IActionResult> AddLearningProcess([FromBody] Request_LearningProcess request)
        {
            return Ok(await _studyService.AddLearningProcess(request));
        }
        [HttpPut("update-learningprocess/{learningId}")]
        public async Task<IActionResult> UpdateLearningProcess([FromRoute] long learningId, Request_LearningProcess request)
        {
            return Ok(await _studyService.UpdateLearningProcess(learningId, request));
        }
        [HttpDelete("delete-learningprocess")]
        public async Task<IActionResult> DeleteLearningProcess([FromRoute] long learningId)
        {
            return Ok(await _studyService.DeleteLearningProcess(learningId));
        }
    }
}
