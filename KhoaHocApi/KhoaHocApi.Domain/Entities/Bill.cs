using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class Bill : BaseEntities
    {
        public double Price {  get; set; }
        public string TradingCode {  get; set; }
        public DateTime CreateTime { get; set; }
        public long UserId {  get; set; }
        public virtual User? User { get; set; }
        public long CourseId {  get; set; }
        public virtual Course? Course { get; set; }
        public long BillStatusId {  get; set; }
        public virtual BillStatus? BillStatus { get; set; }
    }
}
