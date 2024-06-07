using BaiTestPost.Handler.Pagination;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataBill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.InterfaceService
{
    public interface IBillService
    {
        Task<ResponseObject<DataResponseBill>> CreateBill(long CourseID);
        Task<PageResult<DataResponseBill>> GetAllBill( int pageNumer, int pageSize);
        Task<ResponseObject<DataResponseBill>> GetAllBillById(long BillID);
        

    }
}
