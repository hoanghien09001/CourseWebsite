using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Domain.Entities
{
    public class CommentBlog : BaseEntities
    {
        public string Content {  get; set; }
        public bool Edited { get; set; }
        public long UserId {  get; set; }
        public virtual User? User { get; set; }
        public long BlogId {  get; set; }
        public virtual Blog? Blog { get; set; }
    }
}
