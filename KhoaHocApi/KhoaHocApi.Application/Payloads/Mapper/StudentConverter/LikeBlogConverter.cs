using KhoaHocApi.Application.Payloads.ResponseModel.DataStudents;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.Payloads.Mapper.StudentConverter
{
    public class LikeBlogConverter
    {
        private readonly IBaseRepository<Blog> _blogRepository;
        private readonly IBaseRepository<User> _userRepository;

        public LikeBlogConverter(IBaseRepository<Blog> blogRepository,  IBaseRepository<User> userRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        public DataResponseLikeBlog EntityDTO(LikeBlog likeBlog)
        {
            var user = _userRepository.GetByIdAsync(likeBlog.UserId).Result;
            var blog = _blogRepository.GetByIdAsync(likeBlog.BlogId).Result;
            return new DataResponseLikeBlog()
            {
                Id = likeBlog.Id,
                UserName = user.Fullname,
                BlogTitle = blog.Title,

            };
        }
    }
}
