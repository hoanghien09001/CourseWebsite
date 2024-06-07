using BaiTestPost.Handler.Pagination;
using KhoaHocApi.Application.Payloads.RequestModels.DohomeworkRequest;
using KhoaHocApi.Application.Payloads.RequestModels.InputRequest;
using KhoaHocApi.Application.Payloads.RequestModels.PracticeRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataBill;
using KhoaHocApi.Application.Payloads.ResponseModel.DataDohomework;
using KhoaHocApi.Application.Payloads.ResponseModel.DataPractice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.InterfaceService
{
    public interface IPractiveService
    {
        Task<ResponseObject<DataResponsePractice>> CreatePractice(Request_Practicee request);
        Task<ResponseObject<DataResponsePractice>> UpdatePractice(long practiceid, Request_UpdatePractice request);
        Task<ResponseObject<DataResponsePractice>> DeletePractice(long practiceid);
        Task<PageResult<DataResponsePractice>> GetAllPractice(FilterPractice? practice, int pageNumber, int pageSize);
        Task<ResponseObject<DataResponsePractice>> GetPracticeById(long practiceid);
        Task<ResponseObject<DataResponseDohomework>> Dohomework(Request_Dohomework request);
    }
}
