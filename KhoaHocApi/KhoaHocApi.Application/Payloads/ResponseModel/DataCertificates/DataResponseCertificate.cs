using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.ResponseModel.DataCertificates
{
    public class DataResponseCertificate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string CertificateTypeName { get; set; }
    }
}
