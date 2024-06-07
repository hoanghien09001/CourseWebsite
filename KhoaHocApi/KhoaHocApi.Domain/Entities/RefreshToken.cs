using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class RefreshToken : BaseEntities
    {
        public string Token {  get; set; }
        public DateTime ExpiryTime { get; set; }
        public long UserId {  get; set; }
        public virtual User? User { get; set; }
    }
}
