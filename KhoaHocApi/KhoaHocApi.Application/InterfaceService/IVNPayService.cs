using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.InterfaceService
{
    public interface  IVNPayService
    {
        Task<string> CreatePaymentUrl(int billId, HttpContext httpContext, int id);
        Task<string> VNPayReturn(IQueryCollection vnpayData);
    }
}
