using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class ProgramingLanguage : BaseEntities
    {
        public string LanguageName { get; set; }
        public virtual ICollection<Practice>? Practices { get; set; }
        public virtual ICollection<TestCase>? TestCases { get; set; }
    }
}
