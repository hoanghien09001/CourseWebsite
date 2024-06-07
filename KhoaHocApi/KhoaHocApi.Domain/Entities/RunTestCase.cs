using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class RunTestCase : BaseEntities
    {
        public string Result {  get; set; }
        public double Runtime {  get; set; }
        public long DoHomeworkId {  get; set; }
        public virtual DoHomework? DoHomework { get; set; }
        public long TestCaseId {  get; set; }
        public virtual TestCase? TestCase { get; set; }
    }
}
