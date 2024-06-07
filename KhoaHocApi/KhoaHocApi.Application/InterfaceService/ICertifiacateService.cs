
using KhoaHocApi.Application.Payloads.RequestModels.CertificateRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataCertificates;
using KhoaHocApi.Application.Payloads.ResponseModel.DataUsers;
using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.InterfaceService
{
    public interface ICertifiacateService
    {
        Task<IQueryable<CertificateType>> GetAllCertificateType();
        Task<IQueryable<DataResponseTeacher>> GetUserHaveCert();
        Task<ResponseObject<DataResponseCertificateType>> AddCertificateType(Request_CertificateType request);
        Task<ResponseObject<DataResponseCertificateType>> UpdateCertificateType(long id, Request_CertificateType request);
        Task<ResponseObject<DataResponseCertificateType>> DeleteCertificateType(long id);
        Task<IQueryable<Certificate>> GetAllCertificate();
        Task<IQueryable<Certificate>> GetCertificateById(long id);
        Task<ResponseObject<DataResponseCertificate>> AddCertificate(Request_Certificate request);
        Task<ResponseObject<DataResponseCertificate>> UpdateCertificate(long id, Request_Certificate request);
        Task<ResponseObject<DataResponseCertificate>> DeleteCertificate(long id);
    }
}
