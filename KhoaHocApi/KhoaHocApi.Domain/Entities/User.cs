using KhoaHocApi.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class User : BaseEntities
    {
        public string Username {  get; set; }
        public string Password {  get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar {  get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? Fullname {  get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address {  get; set; }
        public bool IsActive {  get; set; }
        public ConstantEnum.UserStatusEnum Status { get; set; } = ConstantEnum.UserStatusEnum.UnActived;
        public bool IsLocked {  get; set; }
        public long? ProvinceId {  get; set; }
        public virtual Province? Province { get; set; }
        public long? DistrictId {  get; set; }
        public virtual District? District { get; set; }
        public long? WardId { get; set; }
        public virtual Ward? Ward { get; set; }
        public long? CertificateId {  get; set; }
        public virtual Certificate? Certificate { get; set; }  
        public virtual ICollection<Permission>? Permissions { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<ConfirmEmail>? ConfirmEmails { get; set; }
        public virtual ICollection<MakeQuestion>? MakeQuestions { get; set; }
        public virtual ICollection<Answers>? Answers { get; set; }
        public virtual ICollection<DoHomework>? DoHomeworks { get; set;}
        public virtual ICollection<Blog>? Blogs { get; set; }
        public virtual ICollection<CommentBlog>? CommentBlogs { get; set; }
        public virtual ICollection<LearningProgress>? LearningProgresses { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<LikeBlog>? LikeBlogs {  get; set; }
        public virtual ICollection<Bill>? Bills { get; set; }

    }
}
