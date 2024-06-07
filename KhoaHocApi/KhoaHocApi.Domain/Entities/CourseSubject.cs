using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class CourseSubject : BaseEntities
    {
        public long CourseId {  get; set; }
        public virtual Course? Course { get; set; }
        public long SubjectId {  get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
