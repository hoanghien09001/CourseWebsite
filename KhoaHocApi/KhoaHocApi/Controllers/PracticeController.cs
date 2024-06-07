using KhoaHocApi.Application.Constant;
using KhoaHocApi.Application.ImplementService;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.RequestModels.DohomeworkRequest;
using KhoaHocApi.Application.Payloads.RequestModels.InputRequest;
using KhoaHocApi.Application.Payloads.RequestModels.PracticeRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoaHocApi.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    
    public class PracticeController : ControllerBase
    {
        private readonly IPractiveService _practiveService;
        public PracticeController(IPractiveService practiveService)
        {
            _practiveService = practiveService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePractice(Request_Practicee request)
        {
            return Ok(await _practiveService.CreatePractice(request));
        }

        [HttpPut]
        public async Task<IActionResult> UpatePractice (long PracticeId, Request_UpdatePractice request)
        {
            return Ok(await _practiveService.UpdatePractice( PracticeId, request));
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePractice(long PracticeId)
        {
            return Ok(await _practiveService.DeletePractice(PracticeId ));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPractice([FromQuery]FilterPractice? practice, int pageNumber=1, int pageSize=10)
        {
            return Ok(await _practiveService.GetAllPractice(practice, pageNumber, pageSize));
        }
        [HttpGet]
        public async Task<IActionResult> GetPracticeById(long PracticeId)
        {
            return Ok(await _practiveService.GetPracticeById(PracticeId));
        }

        [HttpPost]
        public async Task<IActionResult> DoHomeWork([FromQuery]Request_Dohomework request)
        {
            return Ok(await _practiveService.Dohomework(request));
        }
    }
}
