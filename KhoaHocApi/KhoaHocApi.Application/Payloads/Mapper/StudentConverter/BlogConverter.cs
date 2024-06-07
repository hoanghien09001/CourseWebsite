using KhoaHocApi.Application.InterfaceService;
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
    public class BlogConverter
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<CommentBlog> _commentRepository;
        private readonly CommentConverter _commentConverter;
        private readonly IBaseRepository<LikeBlog> _likeRepository;
        private readonly LikeBlogConverter _likeBlogConverter;
        public BlogConverter(IBaseRepository<User> userRepository, IBaseRepository<CommentBlog> commentRepository, CommentConverter commentConverter,
            IBaseRepository<LikeBlog> likeRepository , LikeBlogConverter likeBlogConverter)
        {
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _commentConverter = commentConverter;
            _likeRepository = likeRepository;
            _likeBlogConverter = likeBlogConverter;
        }
        public DataResponseBlog EntityDTO(Blog blog)
        {
            var username = _userRepository.GetByIdAsync(blog.UserId).Result;
            return new DataResponseBlog()
            {
                Id=blog.Id,
                Content= blog.Content,
                Title =blog.Title,
                NumberOfComments= blog.NumberOfComments,
                NumberOfLikes= blog.NumberOfLikes,
                CreateTime= blog.CreateTime,
                UpdateTime=blog.UpdateTime,
                UserName = username.Fullname,
                DataResponseComments = _commentRepository.GetAllAsync(x => x.BlogId == blog.Id).Result.Select(x => _commentConverter.EntityDTO(x)),
                DataResponseLikes = _likeRepository.GetAllAsync(x => x.BlogId == blog.Id && x.Unlike==false).Result.Select(x => _likeBlogConverter.EntityDTO(x))
            };
        }
    
        
    }
}
