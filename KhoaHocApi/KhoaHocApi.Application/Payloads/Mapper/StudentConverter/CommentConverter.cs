using KhoaHocApi.Application.Payloads.ResponseModel.DataStudents;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper.StudentConverter
{
    public class CommentConverter
    {
        protected readonly IBaseRepository<User> _userRepository;
        protected readonly IBaseRepository<Blog> _blogRepository;
        public CommentConverter(IBaseRepository<User> userRepository , IBaseRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }
        public DataResponseComment EntityDTO(CommentBlog commentBlog)
        {
            var username =  _userRepository.GetByIdAsync(commentBlog.UserId).Result;
            var blogTitle =  _blogRepository.GetByIdAsync(commentBlog.BlogId).Result;
            return new DataResponseComment()
            {
                Id=commentBlog.Id,
                UserName = username.Fullname,
                BlogTitle = blogTitle.Title,
                Content = commentBlog.Content,
                Edited = commentBlog.Edited,
            };
        }
    }
}
