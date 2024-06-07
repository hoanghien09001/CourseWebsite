using BaiTestPost.Handler.Pagination;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.Mapper.StudentConverter;
using KhoaHocApi.Application.Payloads.RequestModels.InputRequest;
using KhoaHocApi.Application.Payloads.RequestModels.StudentsRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataStudents;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.DataContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.ImplementService
{
    public class StudentService : IStudentService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Blog> _BlogRepository;
        private readonly IBaseRepository<LikeBlog> _likeBlogRepository;
        private readonly IBaseRepository<CommentBlog> _commentBlogRepository;
        private readonly IBaseRepository<SubjectDetail> _subjectdetailRepository;
        private readonly IBaseRepository<MakeQuestion> _questionlRepository;
        private readonly IBaseRepository<Answers> _answerRepository;
        private readonly IBaseRepository<CourseSubject> _courseSubjectRepository;
        private readonly IBaseRepository<SubjectDetail> _subjectDetailRepository;

        private readonly BlogConverter _blogConverter;
        private readonly CommentConverter _commentConverter;
        private readonly MakeQuestionConerter _makeQuestionConerter;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AnswerConverter _answerConverter;
        private readonly LikeBlogConverter _likeBlogConverter;

        public StudentService( BlogConverter blogConverter, IHttpContextAccessor httpContext,
            CommentConverter commentConverter, MakeQuestionConerter makeQuestionConerter, AnswerConverter answerConverter, 
            IBaseRepository<User> userRepository,IBaseRepository<Blog> blogRepository, IBaseRepository<LikeBlog> likeBlogRepository,
            IBaseRepository<CommentBlog> commentBlogRepository ,IBaseRepository<SubjectDetail> subjectdetailRepository, 
            IBaseRepository<MakeQuestion> questionlRepository, IBaseRepository<Answers> answerRepository , LikeBlogConverter likeBlogConverter,
            IBaseRepository<CourseSubject> courseSubjectRepository, IBaseRepository<SubjectDetail> subjectDetailRepository)
        {

            _blogConverter = blogConverter;
            _httpContext = httpContext;
            _commentConverter = commentConverter;
            _makeQuestionConerter = makeQuestionConerter;
            _answerConverter = answerConverter;
            _userRepository = userRepository;
            _BlogRepository = blogRepository;
            _likeBlogRepository = likeBlogRepository;
            _commentBlogRepository = commentBlogRepository;
            _subjectdetailRepository = subjectdetailRepository;
            _questionlRepository = questionlRepository;
            _answerRepository = answerRepository;
            _likeBlogConverter = likeBlogConverter;
            _courseSubjectRepository = courseSubjectRepository;
            _subjectDetailRepository = subjectDetailRepository;

        }
        #region//CreateBlog
        public async Task<ResponseObject<DataResponseBlog>> CreateBlog(Request_CreateLog request)
        {
            var currentUser = _httpContext.HttpContext.User;
            if(!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseBlog>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if(userLogin.IsActive==false)
            {
                return new ResponseObject<DataResponseBlog>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }
            Blog blog = new Blog()
            {
                
                UserId=idUser,
                Content = request.Content,
                Title = request.Title,
                NumberOfComments = 0,
                NumberOfLikes = 0,
                CreateTime = DateTime.Now,

            };
            blog = await _BlogRepository.CreateAsync(blog);
            return new ResponseObject<DataResponseBlog >
            {
                Status = StatusCodes.Status200OK,
                Message = "Đăng bài thành công!",
                Data = _blogConverter.EntityDTO(blog),
            };
        }
        #endregion

        #region//UpdateBlog
        public async Task<ResponseObject<DataResponseBlog>> UpdateBlog(long IdBlog, Request_CreateLog request)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseBlog>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseBlog>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }

            var BlogUser = await _BlogRepository.GetByIdAsync(IdBlog);
            if(BlogUser==null)
            {
                return new ResponseObject<DataResponseBlog>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Blog không tồn tại!",
                    Data = null,
                };
            }

            BlogUser.Content = request.Content;
            BlogUser.Title = request.Title;
            BlogUser.UpdateTime = DateTime.Now;

            await _BlogRepository.UpdateAsync(BlogUser);
            return new ResponseObject<DataResponseBlog>
            {
                Status = StatusCodes.Status200OK,
                Message = "Cập nhật Blog thành công!",
                Data = _blogConverter.EntityDTO(BlogUser),
            };
        }
        #endregion

        #region//DeleteBlog
        public async Task<ResponseObject<DataResponseBlog>> DeleteBlog(long IdBlog)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseBlog>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser); ;
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseBlog>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }

            var BlogUser = await _BlogRepository.GetByIdAsync(IdBlog);
            if (BlogUser == null)
            {
                return new ResponseObject<DataResponseBlog>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Blog không tồn tại!",
                    Data = null,
                };
            }

            //var likeBlog = _dbcontext.LikeBlogs.Where(x => x.BlogId == IdBlog);
            var likeBlog = await _likeBlogRepository.GetAllAsync(x => x.BlogId == IdBlog);
            if (likeBlog != null)
            {
                foreach(var item in likeBlog)
                {
                    await _likeBlogRepository.DeleteAsync(item.Id);
                }
            }

            var commentBlog = await _commentBlogRepository.GetAllAsync(x=>x.BlogId==IdBlog);
            if (commentBlog != null)
            {
                foreach (var item in commentBlog)
                {
                    await _commentBlogRepository.DeleteAsync(item.Id);
                }
            }
            await _BlogRepository.DeleteAsync(IdBlog);
            return new ResponseObject<DataResponseBlog>
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa Blog thành công!",
                Data =_blogConverter.EntityDTO(BlogUser),
            };
        }
        #endregion

        #region//GetAllBlog
            
        public async Task<PageResult<DataResponseBlog>> GetAlls(FilterBlog? input, int pageNumber, int pageSize)
        {
            var list = await _BlogRepository.GetAllAsync();
            if (input.UserId.HasValue)
            {
                list = list.Where(record => record.UserId == input.UserId).OrderByDescending(x=>x.CreateTime);
            }
            if (!string.IsNullOrEmpty(input.Title))
            {
                list = list.Where(record => record.Title.ToLower().Contains(input.Title.ToLower())).OrderByDescending(x => x.CreateTime);
            }
            var query = list.ToList().Select( item => _blogConverter.EntityDTO(item)).OrderByDescending(x => x.CreateTime);
            var result = Pagination.GetPagedData(query.AsQueryable(), pageNumber, pageSize);
            return result;
        }
        #endregion

        #region//GetBlogById
        public async Task<ResponseObject<DataResponseBlog>> GetBlogById(long IdBlog)
        {
            var blog = await _BlogRepository.GetByIdAsync(x => x.Id == IdBlog);
            if (blog == null)
            {
                return new ResponseObject<DataResponseBlog>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Không tồn tại Blog !",
                    Data = null
                };
            }

            var blogDto = _blogConverter.EntityDTO(blog);

            return new ResponseObject<DataResponseBlog>
            {
                Status = StatusCodes.Status200OK,
                Message = "Hiển thị Blog thành công!",
                Data = blogDto
            };
        }
        #endregion

        //-------------------------

        #region//LikeOrUnlikeBlog
        public async Task<ResponseObject<DataResponseLikeBlog>> LikeOrUnlike(long IdBlog)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseLikeBlog>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseLikeBlog>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }

            var BlogUser = await _BlogRepository.GetByIdAsync(IdBlog);
            if (BlogUser == null)
            {
                return new ResponseObject<DataResponseLikeBlog>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Blog không tồn tại!",
                    Data = null,
                };
            }
            var Likeblog = await _likeBlogRepository.GetByIdAsync(x => x.BlogId == IdBlog);
            if (Likeblog == null)
            {
                LikeBlog likeBlog = new LikeBlog()
                {
                    UserId = idUser,
                    BlogId = IdBlog,
                    CreateTime = DateTime.Now,
                    Unlike = false
                };
                await _likeBlogRepository.CreateAsync(likeBlog);
                BlogUser.NumberOfLikes += 1;
                await _BlogRepository.UpdateAsync(BlogUser);
                return new ResponseObject<DataResponseLikeBlog>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Like thành công!",
                    Data = _likeBlogConverter.EntityDTO(likeBlog),
                };
            }
            else
            {
                if (Likeblog.Unlike == true)
                {
                    Likeblog.Unlike = false;
                    await _likeBlogRepository.UpdateAsync(Likeblog);
                    BlogUser.NumberOfLikes += 1;
                    await _BlogRepository.UpdateAsync(BlogUser);

                    return new ResponseObject<DataResponseLikeBlog>
                    {
                        Status = StatusCodes.Status200OK,
                        Message = "Like thành công!",
                        Data = _likeBlogConverter.EntityDTO(Likeblog),
                    };
                }
                else
                {
                    Likeblog.Unlike = true;
                    await _likeBlogRepository.UpdateAsync(Likeblog);
                    BlogUser.NumberOfLikes -= 1;
                    await _BlogRepository.UpdateAsync(BlogUser);
                    return new ResponseObject<DataResponseLikeBlog>
                    {
                        Status = StatusCodes.Status200OK,
                        Message = "UnLike thành công!",
                        Data = _likeBlogConverter.EntityDTO(Likeblog),
                    };
                }
            }
        }
        #endregion

        #region//CommentBlog
        public async Task<ResponseObject<DataResponseComment>> CommentBlog(long IdBlog, string content)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseComment>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin =await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseComment>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }

            var BlogUser =await _BlogRepository.GetByIdAsync(IdBlog);
            if (BlogUser == null)
            {
                return new ResponseObject<DataResponseComment>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Blog không tồn tại!",
                    Data = null,
                };
            }
            CommentBlog commentBlog = new CommentBlog()
            {
                UserId = idUser,
                BlogId = IdBlog,
                Edited = false,
                Content = content,

            };
            await _commentBlogRepository.CreateAsync(commentBlog);
            BlogUser.NumberOfComments += 1;
            await _BlogRepository.UpdateAsync(BlogUser);
            return new ResponseObject<DataResponseComment>
            {
                Status = StatusCodes.Status200OK,
                Message = "Comment Blog thành công!",
                Data =  _commentConverter.EntityDTO(commentBlog),
            };
        }

        #endregion

        #region//UpdateComment
        public async Task<ResponseObject<DataResponseComment>> UpdateComment(long Idcomment, string content)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseComment>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseComment>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }

            var CommentBlog = await _commentBlogRepository.GetByIdAsync(Idcomment);
            if (CommentBlog == null)
            {
                return new ResponseObject<DataResponseComment>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Comment không tồn tại!",
                    Data = null,
                };
            }
            CommentBlog.Content = content;
            CommentBlog.Edited = true;
            await _commentBlogRepository.UpdateAsync(CommentBlog);
            return new ResponseObject<DataResponseComment>
            {
                Status = StatusCodes.Status200OK,
                Message = "Sửa Comment Blog thành công!",
                Data = _commentConverter.EntityDTO(CommentBlog),
            };
        }
        #endregion

        #region//DeleteComment
        public async Task<ResponseObject<DataResponseComment>> DeleteComment(long Idcomment)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseComment>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }

            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin == null || !userLogin.IsActive)
            {
                return new ResponseObject<DataResponseComment>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }

            var commentToDelete = await _commentBlogRepository.GetByIdAsync(Idcomment);
            if (commentToDelete == null)
            {
                return new ResponseObject<DataResponseComment>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Bình luận không tồn tại!",
                    Data = null,
                };
            }

            var BlogUser = await _BlogRepository.GetByIdAsync(x => x.Id == commentToDelete.BlogId);
            await _commentBlogRepository.DeleteAsync(Idcomment);
            BlogUser.NumberOfComments -= 1;
            await _BlogRepository.UpdateAsync(BlogUser);

            return new ResponseObject<DataResponseComment>
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa bình luận thành công!",
                Data = _commentConverter.EntityDTO(commentToDelete)
            };

        }

        #endregion

        #region//GetAllCommentInBlogId
        public async Task<PageResult<DataResponseComment>> GetAllComment(long IdBlog, int pageNumber, int pageSize)
        {
            var list = await _commentBlogRepository.GetAllAsync(x=>x.BlogId == IdBlog);
            var query = list.ToList().Select(item => _commentConverter.EntityDTO(item));
            var result = Pagination.GetPagedData(query.AsQueryable(), pageNumber, pageSize);
            return result;

        }

        #endregion
        //------------------------

        #region//MakeQuestion
        public async Task<ResponseObject<DataResponseMakeQuestion>> CreateMakeQuest(long subjectdetailId, string question)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }

            var subjectDetail =await _subjectdetailRepository.GetByIdAsync(x => x.Id == subjectdetailId && x.IsActive==true || x.IsFinished==false);
            if (subjectDetail == null)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "subjectdetail không tồn tại!",
                    Data = null,
                };
            }
            MakeQuestion make = new MakeQuestion()
            {
                SubjectDetailId = subjectdetailId,
                NumberOfAnswer = 0,
                CreateTime = DateTime.Now,
                UserId = idUser,
                Question = question,

            };
            await _questionlRepository.CreateAsync(make);
            return new ResponseObject<DataResponseMakeQuestion>
            {
                Status = StatusCodes.Status200OK,
                Message = "Thêm Question thành công",
                Data = _makeQuestionConerter.EntityDTO(make),
            };
        }
        #endregion

        #region//UpdateQuestion
        public async Task<ResponseObject<DataResponseMakeQuestion>> UpdateQuestion(long questionId, string question)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }

            var Question =await _questionlRepository.GetByIdAsync(questionId);
            if (Question == null)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Question không tồn tại!",
                    Data = null,
                };
            }
           
            var subjectDetail = await _subjectdetailRepository.GetByIdAsync(x => x.Id == Question.SubjectDetailId && x.IsActive == true || x.IsFinished == false);
            if (subjectDetail == null)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "subjectdetail không tồn tại!",
                    Data = null,
                };
            }
            Question.Question= question;
            Question.UpdateTime = DateTime.Now;
            await _questionlRepository.UpdateAsync(Question);
            return new ResponseObject<DataResponseMakeQuestion>
            {
                Status = StatusCodes.Status200OK,
                Message = "Update Question thành công",
                Data = _makeQuestionConerter.EntityDTO(Question),
            };
        }
        #endregion

        #region//DeleteQuestion
        public async Task<ResponseObject<DataResponseMakeQuestion>> DeleteQuestion(long questionId)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }

            var Question =await _questionlRepository.GetByIdAsync(questionId);
            if (Question == null)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Question không tồn tại!",
                    Data = null,
                };
            }
            var subjectDetail = await _subjectdetailRepository.GetByIdAsync(x => x.Id == Question.SubjectDetailId && x.IsActive == true || x.IsFinished == false);
            if (subjectDetail == null)
            {
                return new ResponseObject<DataResponseMakeQuestion>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "subjectdetail không tồn tại!",
                    Data = null,
                };
            }
            var Answer =await _answerRepository.GetAllAsync(x => x.MakeQuestionId == questionId);
            if (Answer != null)
            {
                foreach (var item in Answer)
                {
                    await _likeBlogRepository.DeleteAsync(item.Id);
                }
            }

            await _questionlRepository.DeleteAsync(questionId);

            return new ResponseObject<DataResponseMakeQuestion>
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa Question thành công!",
                Data = _makeQuestionConerter.EntityDTO(Question),
            };
        }


        #endregion

        #region//GetAllQuestionInSubjectDetailId
        public async Task<PageResult<DataResponseMakeQuestion>> GetAllQuestion(long IdsubjectDetail, int pageNumber, int pageSize)
        {
            var list = await _questionlRepository.GetAllAsync(x => x.SubjectDetailId == IdsubjectDetail);
            var query = list.ToList().Select(item => _makeQuestionConerter.EntityDTO(item)).OrderByDescending(y=>y.CreateTime);
            var result = Pagination.GetPagedData(query.AsQueryable(), pageNumber, pageSize);
            return result;
        }

        #endregion

        #region//GetQuestionById
        public async Task<ResponseObject<DataResponseMakeQuestion>> GetQuestionById(long questionId)
        {
            var question = await _questionlRepository.GetByIdAsync(questionId);

             if (question == null)
             {
                 return new ResponseObject<DataResponseMakeQuestion>
                 {
                     Status = StatusCodes.Status400BadRequest,
                     Message = "Không tồn tại QuestTion !",
                     Data = null
                 };
             }

             var questionDto = _makeQuestionConerter.EntityDTO(question);

             return new ResponseObject<DataResponseMakeQuestion>
             {
                 Status = StatusCodes.Status200OK,
                 Message = "Hiển thị Question thành công!",
                 Data = questionDto
             };
        }
        #endregion
        //---------------------------

        #region//CreateAnswer
        public async Task<ResponseObject<DataResponseAnswer>> CreateAnswer(long questionid, string answer)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin =await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }
            var Question =await _questionlRepository.GetByIdAsync(questionid);
            if (Question == null)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Question không tồn tại!",
                    Data = null,
                };
            }
            var subjectDetail = await _subjectdetailRepository.GetByIdAsync(x => x.Id == Question.SubjectDetailId && x.IsActive == true || x.IsFinished == false);
            if (subjectDetail == null)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "subjectdetail không tồn tại!",
                    Data = null,
                };
            }
            Answers answers = new Answers()
            {
                UserId = idUser,
                MakeQuestionId = questionid,
                CreateTime = DateTime.Now,
                Answer = answer,
            };
            await _answerRepository.CreateAsync(answers);
            Question.NumberOfAnswer += 1;
            await _questionlRepository.UpdateAsync(Question);
            return new ResponseObject<DataResponseAnswer>
            {
                Status = StatusCodes.Status200OK,
                Message = "Answer thành công!",
                Data = _answerConverter.EntityDTO(answers),
            };
        }

        #endregion

        #region//UpdateAnswer
        public async Task<ResponseObject<DataResponseAnswer>> UpdateAnswer(long answerid, string answer)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }
            var AnswerQt = await _answerRepository.GetByIdAsync(x=>x.Id==answerid);
            if (AnswerQt == null)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Answer không tồn tại!",
                    Data = null,
                };
            }

            var Question = await _questionlRepository.GetByIdAsync(x => x.Id == AnswerQt.MakeQuestionId);
            if (Question == null)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Question không tồn tại!",
                    Data = null,
                };
            }
            var subjectDetail = await _subjectdetailRepository.GetByIdAsync(x => x.Id == Question.SubjectDetailId && x.IsActive == true || x.IsFinished == false);
            if (subjectDetail == null)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "subjectdetail không tồn tại!",
                    Data = null,
                };
            }

            AnswerQt.Answer = answer;
            AnswerQt.UpdateTime = DateTime.Now;
            await _answerRepository.UpdateAsync(AnswerQt);          
            return new ResponseObject<DataResponseAnswer>
            {
                Status = StatusCodes.Status200OK,
                Message = "Update Answer thành công!",
                Data = _answerConverter.EntityDTO(AnswerQt),
            };
        }
        #endregion

        #region//DeleteAnswer
        public async Task<ResponseObject<DataResponseAnswer>> DeleteAnswer(long answerid)
        {
            var currentUser = _httpContext.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Người dùng chưa được xác thực",
                    Data = null,
                };
            }
            var claim = currentUser.FindFirst("Id");
            var idUser = int.Parse(claim.Value);
            var userLogin = await _userRepository.GetByIdAsync(idUser);
            if (userLogin.IsActive == false)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản của bạn đã dừng hoạt động!",
                    Data = null,
                };
            }
            var AnswerQt = await _answerRepository.GetByIdAsync(answerid);
            if (AnswerQt == null)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Answer không tồn tại!",
                    Data = null,
                };
            }

            var Question = await _questionlRepository.GetByIdAsync(x => x.Id == AnswerQt.MakeQuestionId);
            if (Question == null)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Question không tồn tại!",
                    Data = null,
                };
            }
            var subjectDetail = await _subjectdetailRepository.GetByIdAsync(x => x.Id == Question.SubjectDetailId && x.IsActive == true || x.IsFinished == false);
            if (subjectDetail == null)
            {
                return new ResponseObject<DataResponseAnswer>
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "subjectdetail không tồn tại!",
                    Data = null,
                };
            }
            await _answerRepository.DeleteAsync(answerid);
            Question.NumberOfAnswer -= 1;
            await _questionlRepository.UpdateAsync(Question);
            return new ResponseObject<DataResponseAnswer>
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa Answer thành công!",
                Data = _answerConverter.EntityDTO(AnswerQt),
            };
        }
        #endregion

        #region//GetAllAnswer
        public async Task<PageResult<DataResponseAnswer>> GetAlAnswer(long questionId, int pageNumber, int pageSize)
        {
            var list = await _answerRepository.GetAllAsync(x => x.MakeQuestionId == questionId);
            var query = list.ToList().Select(item => _answerConverter.EntityDTO(item)).OrderByDescending(y => y.CreateTime);
            var result = Pagination.GetPagedData(query.AsQueryable(), pageNumber, pageSize);
            return result;
        }

        #endregion
        public async Task<IQueryable<DataResponseMakeQuestion>> GetAllQuestionByCourseId(long courseId)
        {
            var listSubjects = await _courseSubjectRepository.GetAllAsync(x=>x.CourseId == courseId);
            List<DataResponseMakeQuestion> listQuestions = new List<DataResponseMakeQuestion>();
            foreach (var subject in listSubjects)
            {
                var listSubjectDetails = await _subjectDetailRepository.GetAllAsync(x=>x.SubjectId == subject.Id);
                foreach (var subjectDetail in listSubjectDetails)
                {
                    var lstQues = await _questionlRepository.GetAllAsync(x=>x.SubjectDetailId==subjectDetail.Id);
                    var query = lstQues.ToList().Select(item => _makeQuestionConerter.EntityDTO(item)).OrderBy(y => y.CreateTime);
                    listQuestions.AddRange(query);
                }
            }
            var result= listQuestions.OrderBy(x => x.CreateTime).AsQueryable();
            return result;
        }
    }
}
