using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class LearningProgress: BaseEntities
    {
        public long UserId {  get; set; }
        public virtual User? User { get; set; }
        public long RegisterStudyId {  get; set; }
        public virtual RegisterStudy? RegisterStudy { get; set; }
        public long SubjectId {  get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
