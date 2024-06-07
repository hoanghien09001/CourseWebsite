using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.RequestModels.CertificateRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KhoaHocApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertifiacateService _certifiacateService;
        public CertificateController(ICertifiacateService certifiacateService)
        {
            _certifiacateService = certifiacateService;
        }

        [HttpGet("get-all-type")]
        public async Task<IActionResult> GetAllCertificateType()
        {
            return Ok(await _certifiacateService.GetAllCertificateType());
        }

        [HttpGet("get-user-have-certificate")]
        public async Task<IActionResult> GetUserHaveCertificate()
        {
            return Ok(await _certifiacateService.GetUserHaveCert());
        }

        [HttpPost("add-type")]
        public async Task<IActionResult> AddCertificateType([FromBody] Request_CertificateType request)
        {
            return Ok(await _certifiacateService.AddCertificateType(request));
        }

        [HttpPut("update-type/{id}")]
        public async Task<IActionResult> UpdateCertificateType([FromRoute] long id, [FromBody] Request_CertificateType request)
        {
            return Ok(await _certifiacateService.UpdateCertificateType(id, request));
        }

        [HttpDelete("delete-type/{id}")]
        public async Task<IActionResult> DeleteCertificateType([FromRoute] long id)
        {
            return Ok(await _certifiacateService.DeleteCertificateType(id));
        }

        [HttpGet("get-all-certificate")]
        public async Task<IActionResult> GetAllCertificate()
        {
            return Ok(await _certifiacateService.GetAllCertificate());
        }

        [HttpGet("get-certificate/{id}")]
        public async Task<IActionResult> GetCertificateById([FromRoute] long id)
        {
            return Ok(await _certifiacateService.GetCertificateById(id));
        }

        [HttpPost("add-certificate")]
        public async Task<IActionResult> AddCertificate([FromForm] Request_Certificate request)
        {
            return Ok(await _certifiacateService.AddCertificate(request));
        }

        [HttpPut("update-certificate/{id}")]
        public async Task<IActionResult> UpdateCertificate([FromRoute] long id, [FromForm] Request_Certificate request)
        {
            return Ok(await _certifiacateService.UpdateCertificate(id, request));
        }

        [HttpDelete("delete-certificate/{id}")]
        public async Task<IActionResult> DeleteCertificate([FromRoute] long id)
        {
            return Ok(await _certifiacateService.DeleteCertificate(id));
        }
    }
}
