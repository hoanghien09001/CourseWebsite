using KhoaHocApi.Application.Payloads.RequestModels.StudyRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataStudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.InterfaceService
{
    public interface IStudyService
    {
        //RegisterStudy
        Task<IQueryable<DataResponseStudy>> GetAllRegisterStudy();
        Task<ResponseObject<DataResponseStudy>> GetRegisterStudyById(long registerId);
        Task<ResponseObject<DataResponseStudy>> AddRegisterSudy(long userId, Request_Study request);
        Task<ResponseObject<DataResponseStudy>> UpdateRegisterStudy(long registerId,  Request_Study request);
        Task<ResponseObject<DataResponseStudy>> DeleteRegisterStudy(long registerId);
        //LearningProcess
        Task<ResponseObject<DataResponseLearningProcess>> AddLearningProcess(Request_LearningProcess request);
        Task<ResponseObject<DataResponseLearningProcess>> UpdateLearningProcess(long learningProcessId,  Request_LearningProcess request);
        Task<ResponseObject<DataResponseLearningProcess>> DeleteLearningProcess(long learningProcessId);
    }
}
