using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class Blog : BaseEntities
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public int NumberOfLikes {  get; set; }
        public int NumberOfComments {  get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UserId {  get; set; }
        public virtual User? User { get; set; } 
        public virtual ICollection<CommentBlog>? CommentBlogs { get; set; }
        public virtual ICollection<LikeBlog>? LikeBlogs { get; set; }
    }
}
