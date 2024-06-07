using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class TestCase : BaseEntities
    {
        public string Input {  get; set; }
        public string Output { get; set; }
        public long ProgramingLanguageId {  get; set; }
        public virtual ProgramingLanguage? ProgramingLanguage { get; set; }
        public long PracticeId {  get; set; }
        public virtual Practice? Practice { get; set; }

        public virtual ICollection<RunTestCase>? RunTestCases { get; set; }
    }
}
