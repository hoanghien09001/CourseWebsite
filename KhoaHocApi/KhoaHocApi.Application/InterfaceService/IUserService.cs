using KhoaHocApi.Application.Payloads.RequestModels.UserRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.InterfaceService
{
    public interface IUserService
    {
        Task<ResponseObject<DataResponseUser>> UpdateUser(long userId,Request_UpdateUser request);
    }
}
