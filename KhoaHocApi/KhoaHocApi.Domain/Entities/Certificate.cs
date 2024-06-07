using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class Certificate : BaseEntities
    {
        public string Name {  get; set; }
        public string Description {  get; set; }
        public string Image {  get; set; }
        public long CertificateTypeId {  get; set; }
        public virtual CertificateType? CertificateType { get; set; }
    }
}
