using KhoaHocApi.Application.Payloads.ResponseModel.DataCertificates;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper
{
    public class CertificateConverter
    {
        private readonly ICertificateRepository _baseRepository;
        public CertificateConverter(ICertificateRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public DataResponseCertificate EntityDTO(Certificate certificate) 
        {
            return new DataResponseCertificate()
            {
                Name = certificate.Name,
                Description = certificate.Description,
                Image = certificate.Image,
                CertificateTypeName = _baseRepository.GetCertificateNameTypeAsync(certificate.CertificateTypeId).ToString()
            };
        }
    }
}
