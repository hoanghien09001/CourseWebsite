using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Infrastructure.ImplementRepositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly ApplicationDbContext _context;
        public CertificateRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> GetCertificateNameTypeAsync(long id)
        {
            var certificateNameType = await _context.Set<CertificateType>().Where(ct => ct.Id == id).Select(ct => ct.Name).FirstOrDefaultAsync();          
            return certificateNameType;
        }
    }
}
