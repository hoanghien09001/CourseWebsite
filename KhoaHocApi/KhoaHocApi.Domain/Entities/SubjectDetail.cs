using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class SubjectDetail : BaseEntities
    {
        public string SubjectDetailName { get; set; }
        public bool IsFinished {  get; set; }
        public string LinkVideo { get; set; }
        public bool IsActive {  get; set; }
        public long SubjectId {  get; set; }    
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<Practice>? Practices { get; set; }
        public virtual ICollection<MakeQuestion>? MakeQuestions { get; set; }
    }
}
