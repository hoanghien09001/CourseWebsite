using KhoaHocApi.Application.Handle.HandleFile;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.Mapper;
using KhoaHocApi.Application.Payloads.RequestModels.CourseRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataCourse;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhoaHocApi.Application.ImplementService
{
    public class CourseService : ICourseService
    {
        private readonly IBaseRepository<Course> _baseCourseRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly CourseConverter _courseConverter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CourseService(IBaseRepository<Course> baseCourseRepository, CourseConverter courseConverter, IBaseRepository<User> baseUserRepository, IHttpContextAccessor httpContextAccessor)
        {
            _baseCourseRepository = baseCourseRepository;
            _courseConverter = courseConverter;
            _baseUserRepository = baseUserRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseObject<DataResponseCourse>> AddCourse(long userId,Request_Course request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (currentUser == null || !currentUser.IsInRole("Teacher"))
            {
                return new ResponseObject<DataResponseCourse>()
                {
                    Status = StatusCodes.Status403Forbidden,
                    Message = "Bạn không có quyền thực hiện thao tác này!",
                    Data = null
                };
            }
            var user = await _baseUserRepository.GetByIdAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception("Người dùng không tồn tại");
            }
            var course = new Course()
            {
                CourseName = request.CourseName,
                Introduce = request.Introduce,
                ImageCourse = await HandleUploadFile.Upfile(request.ImageCourse),
                Code = request.Code,
                Price = request.Price,
                UserId = userId,
                TotalCourseDuration = request.TotalCourseDuration,
                NumberOfStudent =0,
                NumberOfPurchases = 0
            };
            var checkName = await _baseCourseRepository.GetAsync(x => x.CourseName == course.CourseName);
            if (checkName != null)
            {
                return new ResponseObject<DataResponseCourse>()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Khóa học này đã tồn tại!",
                    Data = null
                };
            }
            await _baseCourseRepository.CreateAsync(course);
            return new ResponseObject<DataResponseCourse>()
            {
                Status = StatusCodes.Status200OK,
                Message = "Thêm khóa học mới thành công",
                Data = _courseConverter.EntityDTO(course)
            };
        }

        public async Task<ResponseObject<DataResponseCourse>> DeleteCourse(long id)
        {
            var currentCourse = await _baseCourseRepository.GetByIdAsync(x => x.Id == id);
            if (currentCourse == null)
            {
                return new ResponseObject<DataResponseCourse>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Khóa học này không tồn tại!",
                    Data = null
                };
            }
            await _baseCourseRepository.DeleteAsync(id);
            return new ResponseObject<DataResponseCourse>()
            {
                Status = StatusCodes.Status200OK,
                Message = "Xóa khóa học thành công",
                Data = _courseConverter.EntityDTO(currentCourse)
            };
        }

        public async Task<IEnumerable<Course>> SearchCourse(string? courseName, string? code, double? minPrice, double? maxPrice)
        {
            var query = await _baseCourseRepository.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(courseName))
            {
                query = query.Where(x => x.CourseName.Contains(courseName));
            }
            if (!string.IsNullOrWhiteSpace(code))
            {
                query = query.Where(x => x.Code.Contains(code));
            }
            if (minPrice.HasValue)
            {
                query = query.Where(x => x.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= minPrice.Value);
            }
            IQueryable<Course> courses = query.AsQueryable();
            return courses;
        }
        public async Task<ResponseObject<DataResponseCourse>> GetCourseById(long id)
        {
            var course = await _baseCourseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return new ResponseObject<DataResponseCourse>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Không tìm thấy khóa học nào!",
                    Data = null
                };
            }
            return new ResponseObject<DataResponseCourse>
            {
                Status = StatusCodes.Status200OK,
                Message = "Thông tin khóa học",
                Data = _courseConverter.EntityDTO(course)
            };
        }
        public async Task<ResponseObject<DataResponseCourse>> UpdateCourse(long id, Request_Course request)
        {
            var currentCourse = await _baseCourseRepository.GetByIdAsync(x => x.Id == id);
            if (currentCourse == null)
            {
                return new ResponseObject<DataResponseCourse>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Khóa học này không tồn tại!",
                    Data = null
                };
            }
            currentCourse.CourseName = request.CourseName;
            currentCourse.Introduce = request.Introduce;
            currentCourse.Code = request.Code;
            currentCourse.Price = request.Price;
            var checkUser = await _baseUserRepository.GetByIdAsync(x => x.Id == currentCourse.UserId);
            if (checkUser == null)
            {
                return new ResponseObject<DataResponseCourse>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Người dùng này không tồn tại!",
                    Data = null
                };
            }
            await _baseCourseRepository.UpdateAsync(currentCourse);
            return new ResponseObject<DataResponseCourse>
            {
                Status = StatusCodes.Status200OK,
                Message = "Cập nhật khóa học thành công",
                Data = _courseConverter.EntityDTO(currentCourse)
            };

        }
        public async Task<ResponseObject<IEnumerable<DataResponseCourse>>> GetAllCourseByTeacher()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user == null || !user.IsInRole("Teacher"))
            {
                return new ResponseObject<IEnumerable<DataResponseCourse>>()
                {
                    Status = StatusCodes.Status403Forbidden,
                    Message = "Bạn không có quyền thực hiện thao tác này!",
                    Data = null
                };
            }

            var listCourse = await _baseCourseRepository.GetAllAsync(x => x.UserId == long.Parse(user.FindFirst("Id").Value));
            if (listCourse == null || listCourse.Count() == 0)
            {
                return new ResponseObject<IEnumerable<DataResponseCourse>>()
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Không tìm thấy khóa học nào",
                    Data = null
                };
            }
            else
            {
                var listCourseDTO = listCourse.Select(x => _courseConverter.EntityDTO(x));
                return new ResponseObject<IEnumerable<DataResponseCourse>>()
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đã tìm thấy danh sách Khóa học",
                    Data = listCourseDTO.AsEnumerable(),
                };
            }
        }

        public async Task<ResponseObject<IEnumerable<DataResponseCourse>>> GetAllCourse()
        {
            var listCourse = await _baseCourseRepository.GetAllAsync();
            var listCourseDTO = listCourse.Select(x=>_courseConverter.EntityDTO(x));
            return new ResponseObject<IEnumerable<DataResponseCourse>>()
            {
                Status = StatusCodes.Status200OK,
                Message = "Lấy danh sách khóa học thành công",
                Data = listCourseDTO.AsEnumerable(),
            };
        }
    }
}