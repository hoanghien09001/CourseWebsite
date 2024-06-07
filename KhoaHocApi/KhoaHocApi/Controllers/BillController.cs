using KhoaHocApi.Application.Constant;
using KhoaHocApi.Application.ImplementService;
using KhoaHocApi.Application.InterfaceService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace KhoaHocApi.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IVNPayService _vnpayService;

        public BillController(IBillService billService , IVNPayService vnpayService)
        {
            _billService = billService;
            _vnpayService = vnpayService;
        }

        //CreateBill
        [HttpPost]
        public async Task<IActionResult> CreateBill(long CourseId)
        {
            return Ok(await _billService.CreateBill(CourseId));
        }
        //GetAllBill
        [HttpGet]
        public async Task<IActionResult> GetAllBill( int pageNumer=1, int pageSize=10)
        {
            return Ok(await _billService.GetAllBill(pageNumer,pageSize));
        }


        //GetBillById
        [HttpGet]
        public async Task<IActionResult> GetBillById(long BillId)
        {
            return Ok(await _billService.GetAllBillById(BillId));
        }

        [HttpGet]
        [Route("/Vnpay/return")]
        
        public async Task<IActionResult> Return()
        {
            var vnpayData = HttpContext.Request.Query;

            return Ok(await _vnpayService.VNPayReturn(vnpayData));
        }
        [HttpPost]
        [Route("/Vnpay/CreatePaymentUrl")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreatePaymentUrl(int billId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _vnpayService.CreatePaymentUrl(billId, HttpContext, id));
        }
    }
}
