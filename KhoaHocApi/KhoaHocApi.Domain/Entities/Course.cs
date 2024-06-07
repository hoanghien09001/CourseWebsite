using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class Course:BaseEntities
    {
        public string CourseName {  get; set; }
        public string Introduce {  get; set; }
        public string ImageCourse {  get; set; }
        public string Code { get; set; }
        public double Price {  get; set; }
        public int TotalCourseDuration { get; set; }
        public int NumberOfStudent {  get; set; }
        public int NumberOfPurchases { get; set; }
        public long UserId {  get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CourseSubject>? CourseSubjects { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
