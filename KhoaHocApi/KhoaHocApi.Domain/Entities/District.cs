using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class District : BaseEntities
    {
        public string Name {  get; set; }
        public long ProvinceId {  get; set; }
        public virtual Province? Province { get; set; }
        public virtual ICollection<Ward>? Wards { get; set; }
        public virtual ICollection<User>? Users { get; set; }

    }
}
