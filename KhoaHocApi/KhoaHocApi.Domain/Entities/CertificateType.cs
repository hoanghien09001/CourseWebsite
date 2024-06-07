using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class CertificateType : BaseEntities
    {
        public string Name {  get; set; }
        public virtual ICollection<Certificate>? Certificates { get; set;}
    }
}
