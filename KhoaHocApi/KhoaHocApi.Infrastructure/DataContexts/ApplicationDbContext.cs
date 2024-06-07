using KhoaHocApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Infrastructure.DataContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext() { }
        public virtual DbSet<User> Users {  get; set; }
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }  
        public virtual DbSet<BillStatus> BillStatuses { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<CertificateType> CertificateTypes { get; set; }
        public virtual DbSet<CommentBlog> CommentBlogs { get; set; }
        public virtual DbSet<ConfirmEmail> ConfirmEmails { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseSubject> CourseSubjects { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DoHomework> DoHomeworks { get; set; }
        public virtual DbSet<LearningProgress> LearningProgresses { get; set; }
        public virtual DbSet<LikeBlog> LikeBlogs { get; set; }
        public virtual DbSet<MakeQuestion> MakeQuestions { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Practice> Practices { get; set; }
        public virtual DbSet<ProgramingLanguage> ProgramingLanguages { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }  
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<RegisterStudy> RegisterStudies { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RunTestCase> RunTestCases { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectDetail> SubjectDetails { get; set; }
        public virtual DbSet<TestCase> TestCases { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }

        public async Task<int> CommitChangeAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
