using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class Ward : BaseEntities
    {
        public string Name { get; set; }
        public long DistrictId {  get; set; }
        public virtual District? District { get; set; }
        public virtual ICollection<User>? Users { get; set; }

    }
}
