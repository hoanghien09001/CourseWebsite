using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class Province : BaseEntities
    {
        public string Name {  get; set; }
        public virtual ICollection<District>? Districts { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
