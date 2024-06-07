using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class Subject : BaseEntities
    {
        public string SubjectName {  get; set; }
        public string Symbol { get; set; } //Ký hiệu
        public bool IsActive { get; set; }
        public virtual ICollection<CourseSubject>? CourseSubjects { get; set; }
        public virtual ICollection<SubjectDetail>? SubjectDetails { get; set; }
        public virtual ICollection<LearningProgress>? LearningProgresses { get; set; }
    }
}
