using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class ConfirmEmail : BaseEntities
    {
        public string ConfirmCode {  get; set; }
        public DateTime ExpiryTime { get; set; }
        public long UserId {  get; set; }
        public virtual User? User { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
