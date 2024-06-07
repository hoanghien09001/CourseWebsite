using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class RegisterStudy : BaseEntities
    {
        public bool IsFinished { get; set; }
        public DateTime RegisterTime {  get; set; }
        public int PercentComplete { get; set; }
        public DateTime DoneTime { get; set; }
        public bool IsActive {  get; set; }
        public long UserId {  get; set; }
        public virtual User? User { get; set; }
        public long CourseId {  get; set; }
        public virtual Course? Course { get; set; }
        public long SubjectId {  get; set; } //Môn học hiện tại
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<DoHomework>? DoHomeworks { get; set; }
        public virtual ICollection<LearningProgress>? LearningProgresses { get; set; }
    }
}
