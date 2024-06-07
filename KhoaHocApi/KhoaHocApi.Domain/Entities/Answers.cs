using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class Answers: BaseEntities
    {
        public string Answer {  get; set; }
        public DateTime CreateTime {  get; set; }
        public DateTime UpdateTime { get; set; }
        public long MakeQuestionId {  get; set; }
        public virtual MakeQuestion? MakeQuestion { get; set; }
        public long UserId {  get; set; }
        public virtual User? User { get; set; }
    }
}
