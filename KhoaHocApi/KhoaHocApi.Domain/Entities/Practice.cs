using KhoaHocApi.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class Practice : BaseEntities
    {
        public ConstantEnum.LevelEnum Level { get; set; }
        public string PracticeCode { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string ExpectOutput {  get; set; }
        public bool IsRequired {  get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsDeleted {  get; set; }
        public double MediumScore {  get; set; }
        public long SubjectDetailId { get; set; }
        public virtual SubjectDetail? SubjectDetail { get; set; }
        public long ProgramingLannguageId {  get; set; }
        public virtual ProgramingLanguage? ProgramingLanguage { get; set; }
        public virtual ICollection<DoHomework>? DoHomeworks { get; set; }
        public virtual ICollection<TestCase>? TestCases { get; set; }
    }
}
