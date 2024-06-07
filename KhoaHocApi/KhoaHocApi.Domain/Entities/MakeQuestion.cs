using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class MakeQuestion : BaseEntities
    {
        public string Question {  get; set; }
        public int NumberOfAnswer { get; set; }
        public DateTime CreateTime {  get; set; }
        public DateTime UpdateTime { get; set; }
        public long UserId {  get; set; }
        public virtual User? User { get; set; }
        public long SubjectDetailId {  get; set; }
        public virtual SubjectDetail? SubjectDetail { get; set;}
        public virtual ICollection<Answers>? Answers { get; set; }
    }
}
