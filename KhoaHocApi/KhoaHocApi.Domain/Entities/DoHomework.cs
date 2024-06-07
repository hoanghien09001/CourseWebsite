using KhoaHocApi.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class DoHomework : BaseEntities
    {
        public ConstantEnum.HomeworkEnum HomeworkStatus { get; set; }
        public bool IsFinished {  get; set; }
        public string ActualOutput { get; set; }
        public DateTime DoneTime { get; set; }
        public long PracticeId {  get; set; }
        public virtual Practice? Practice { get; set; }
        public long UserId {  get; set; }
        public virtual User? User { get; set;}
        public long RegisterStudyId {  get; set; }
        public virtual RegisterStudy? RegisterStudy { get; set;}
        public virtual ICollection<RunTestCase>? RunTestCases { get; set; }
    }
}
