using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class BillStatus: BaseEntities
    {
        public string BillStatusName {  get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
