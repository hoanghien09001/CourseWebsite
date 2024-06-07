using KhoaHocApi.Application.Payloads.ResponseModel.DataCertificates;
using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper
{
    public class CertificateTypeConverter
    {
        public DataResponseCertificateType EntityDTO(CertificateType type)
        {
            return new DataResponseCertificateType()
            {
                Name = type.Name,
            };
        }
    }
}
