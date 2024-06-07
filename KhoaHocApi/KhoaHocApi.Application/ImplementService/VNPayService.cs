using KhoaHocApi.Application.Handle.HandleVnPay;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.DataContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.ImplementService
{
    public class VNPayService : IVNPayService
    {
        private readonly IConfiguration _configuration;
        private readonly VNPayLibrary pay;
        //private readonly ApplicationDbContext _dbContext;
        private readonly IBaseRepository<Bill> _billRepository;
        private readonly IBaseRepository<User> _userRepository;

        public VNPayService(IConfiguration configuration,IBaseRepository<Bill> billRepository , IBaseRepository<User> userRepository)
        {
            _configuration = configuration;
            pay = new VNPayLibrary();
            _billRepository = billRepository;
            _userRepository = userRepository;
        }

        public async Task<string> CreatePaymentUrl(int billId, HttpContext httpContext, int id)
        {
            var bill = await _billRepository.GetByIdAsync(billId);
            var user = await _userRepository.GetByIdAsync(id);
            if(user.Id==bill.UserId)
            {
                if(bill.BillStatusId==2)
                {
                    return "Hóa đơn đã được thanh toán trước đó";
                }
            }
            if(bill.BillStatusId==1 && bill.Price !=null)
            {
                pay.AddRequestData("vnp_Version", "2.1.0");
                pay.AddRequestData("vnp_Command", "pay");
                pay.AddRequestData("vnp_TmnCode", "YIK14C5R");
                pay.AddRequestData("vnp_Amount", (bill.Price * 100).ToString());
                pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", "VND");
                pay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(httpContext));
                pay.AddRequestData("vnp_Locale", "vn");
                pay.AddRequestData("vnp_OrderInfo", $"Thanh taons hóa đơn{billId}");
                pay.AddRequestData("vnp_OrderType", "other");
                pay.AddRequestData("vnp_ReturnUrl", _configuration.GetSection("VnPay:vnp_ReturnUrl").Value);
                pay.AddRequestData("vnp_TxnRef", billId.ToString());

                string paymentUrl = pay.CreateRequestUrl(_configuration.GetSection("VnPay:vnp_Url").Value, _configuration.GetSection("VnPay:vnp_HashSecret").Value);
                return paymentUrl;
            }
            else
            {
                return "Vui lòng kiểm tra lại hóa đơn";
            }
            return "Vui lòng kiểm tra lại hóa đơn";
        }       

        public async Task<string> VNPayReturn(IQueryCollection vnpayData)
        {
            string vnp_TmnCode = _configuration.GetSection("VnPay:vnp_TmnCode").Value;
            string vnp_HashSecret = _configuration.GetSection("VnPay:vnp_HashSecret").Value;

            VNPayLibrary vnPayLibrary = new VNPayLibrary();
            foreach (var (key, value) in vnpayData)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnPayLibrary.AddResponseData(key, value);
                }
            }

            string billId = vnPayLibrary.GetResponseData("vnp_TxnRef");
            string vnp_ResponseCode = vnPayLibrary.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnPayLibrary.GetResponseData("vnp_TransactionStatus");
            string vnp_SecureHash = vnPayLibrary.GetResponseData("vnp_SecureHash");
            double vnp_Amount = Convert.ToDouble(vnPayLibrary.GetResponseData("vnp_Amount"));
            bool check = vnPayLibrary.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            if(check)
            {
                if(vnp_ResponseCode=="00" && vnp_TransactionStatus=="00")
                {
                    var bill = await _billRepository.GetByIdAsync(x => x.Id == Convert.ToInt32(billId));

                    if(bill == null)
                    {
                        return "Không tìm thấy hóa đơn";
                    }
                    bill.BillStatusId = 2;
                    bill.CreateTime = DateTime.Now;
                    await _billRepository.UpdateAsync(bill);
                    /*
                     var user = _context.users.FirstOrDefault(x => x.Id == bill.CustomerId);
                    if (user != null)
                    {
                        string email = user.Email;
                        string mss =  _authService.SendEmail(new EmailTo
                        {
                            Mail = email,
                            Subject = $"Thanh Toán đơn hàng: {bill.Id}",
                            Content = BillEmailTemplate.GenerateNotificationBillEmail(bill, "THANH TOÁN")
                        });
                    }
                     */

                    return "Giao dịch thành công đơn hàng " + bill.Id;
                }
                else
                {
                    return $"Lỗi trong khi thực hiện giao dịch. Mã lỗi: {vnp_ResponseCode}";
                }
            }
            else
            {
                return "Có lỗi trong quá trình xử lý";
            }
        }
    }
}
