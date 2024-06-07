using KhoaHocApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.InterfaceRepositories
{
    public interface ICertificateRepository
    {
        Task<string> GetCertificateNameTypeAsync(long id);        
    }
}
