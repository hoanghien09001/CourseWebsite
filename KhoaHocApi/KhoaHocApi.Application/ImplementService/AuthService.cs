using KhoaHocApi.Application.Handle.HandleEmail;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.Mapper;
using KhoaHocApi.Application.Payloads.RequestModels.UserRequest;
using KhoaHocApi.Application.Payloads.Response;
using KhoaHocApi.Application.Payloads.ResponseModel.DataUsers;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.Enumerates;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Domain.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace KhoaHocApi.Application.ImplementService
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly UserConverter _userConverter;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IBaseRepository<ConfirmEmail> _baseConfirmEmailRepository;
        private readonly IBaseRepository<Permission> _basePermissionRepository;
        private readonly IBaseRepository<Role> _baseRoleRepository;
        private readonly IBaseRepository<RefreshToken> _baseRefreshTokenRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(IBaseRepository<User> baseUserRepository, UserConverter userConverter, IConfiguration configuration, IUserRepository userRepository, IEmailService emailService, IBaseRepository<ConfirmEmail> baseConfirmEmailRepository, IBaseRepository<Permission> basePermissionRepository, IBaseRepository<Role> baseRoleRepository, IBaseRepository<RefreshToken> baseRefreshTokenRepository, IHttpContextAccessor contextAccessor)
        {
            _baseUserRepository = baseUserRepository;
            _userConverter = userConverter;
            _configuration = configuration;
            _userRepository = userRepository;
            _emailService = emailService;
            _baseConfirmEmailRepository = baseConfirmEmailRepository;
            _basePermissionRepository = basePermissionRepository;
            _baseRoleRepository = baseRoleRepository;
            _baseRefreshTokenRepository = baseRefreshTokenRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<ResponseObject<DataResponseUser>> Register(Request_Register request)
        {
            try
            {
                if(!ValidateInput.IsValidEmail(request.Email))          //Email sai định dạng
                {
                    return new ResponseObject<DataResponseUser>()
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Định dạng email không hợp lệ",
                        Data = null,
                    };
                }
                if(await _userRepository.GetUserByEmail(request.Email) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Email đã tồn tại trong hệ thống vui lòng sử dụng email khác",
                        Data = null,
                    };
                }
                if (await _userRepository.GetUserByUsername(request.Username) != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Username đã tồn tại trong hệ thống vui lòng sử dụng username khác",
                        Data = null,
                    };
                }
                var user = new User
                {
                    Avatar = "data:image/png;base64,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",
                    Username = request.Username,
                    Email = request.Email,
                    CreateTime = DateTime.Now,
                    Fullname = request.Fullname,
                    Password = BCryptNet.HashPassword(request.Password),
                    IsActive = true,
                    Status = Domain.Enumerates.ConstantEnum.UserStatusEnum.UnActived,
                    IsLocked = false,
                    
                };
                user = await _baseUserRepository.CreateAsync(user);
                await _userRepository.AddRoleToUserAsync(user, new List<string> { "User"});
                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    IsConfirmed = false,
                    ConfirmCode = GenerateCodeActive(),
                    ExpiryTime = DateTime.Now.AddMinutes(5),
                    UserId = user.Id,
                };
                confirmEmail = await _baseConfirmEmailRepository.CreateAsync(confirmEmail);
                var message = new EmailMessage(new string[] { request.Email }, "Nhận mã xác nhận tại đây", $"Mã xác nhận: {confirmEmail.ConfirmCode}");
                var responseMessage = _emailService.SendEmail(message);
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status201Created,
                    Message = "Bạn đã gửi yêu cầu đăng ký! Vui lòng nhận mã xác nhận tại Email để đăng ký tài khoản",
                    Data = _userConverter.EntityToDTO(user),
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<string> ConfirmRegisterAccount(string confirmCode)
        {
            try
            {
                var code = await _baseConfirmEmailRepository.GetAsync(x=>x.ConfirmCode.Equals(confirmCode));
                if(code == null)
                {
                    return "Mã xác nhận không hợp lệ";
                }
                var user = await _baseUserRepository.GetAsync(x=>x.Id==code.UserId);
                if (code.ExpiryTime < DateTime.Now)
                {
                    return "Mã xác nhận đã hết hạn";
                }
                user.Status = Domain.Enumerates.ConstantEnum.UserStatusEnum.Actived;
                code.IsConfirmed = true;
                await _baseUserRepository.UpdateAsync(user);
                await _baseConfirmEmailRepository.UpdateAsync(code);
                return "Xác nhận đăng ký tài khoản thành công, từ giờ bạn có thể sử dụng tài khoản này để đăng nhập";
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string GenerateCodeActive()             //Tạo code active
        {
            string str = "HoangHien_" + DateTime.Now.Ticks.ToString();
            return str;
        }

        public async Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(User user)
        {
            var permissions = await _basePermissionRepository.GetAllAsync(x=>x.UserId==user.Id);
            var roles = await _baseRoleRepository.GetAllAsync();
            var authClaims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Username", user.Username),
                new Claim("Email", user.Email.ToString()),
                new Claim("FullName", user.Fullname.ToString()),

            };
            foreach (var permission in permissions)
            {
                foreach (var role in roles)
                {
                    if (role.Id == permission.RoleId)
                    {
                        authClaims.Add(new Claim("Permission",role.RoleName));
                    }
                }
            }
            var userRoles = await _userRepository.GetRoleOfUserAsync(user);
            foreach (var item in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item));
            }
            var jwtToken=GetToken(authClaims);
            var refreshToken = GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidity"], out int refreshTokenValidity);

            RefreshToken rf = new RefreshToken
            {
                ExpiryTime = DateTime.Now.AddHours(refreshTokenValidity),
                UserId = user.Id,
                Token = refreshToken,
            };
            rf = await _baseRefreshTokenRepository.CreateAsync(rf);
            return new ResponseObject<DataResponseLogin>
            {
                Status = StatusCodes.Status200OK,
                Message = "Tạo token thành công",
                Data = new DataResponseLogin
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    RefreshToken = refreshToken,
                }
            };
        }

        public async Task<ResponseObject<DataResponseLogin>> Login(Request_Login request)
        {
            var user = await _baseUserRepository.GetAsync(x=>x.Username.Equals(request.Username));
            if(user == null)
            {
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Sai tên tài khoản",
                    Data = null,
                };
            }
            if(user.Status.ToString().Equals(ConstantEnum.UserStatusEnum.UnActived.ToString()))
            {
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Tài khoản chưa được xác thực",
                    Data = null,
                };
            }
            bool checkPass = BCryptNet.Verify(request.Password, user.Password);
            if(!checkPass)
            {
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Mật khẩu không chính xác",
                    Data = null,
                };
            }
            return new ResponseObject<DataResponseLogin>
            {
                Status = StatusCodes.Status200OK,
                Message = "Đăng nhập thành công",
                Data = new DataResponseLogin
                {
                    AccessToken = GetJwtTokenAsync(user).Result.Data.AccessToken,
                    RefreshToken = GetJwtTokenAsync(user).Result.Data.RefreshToken
                }
            };
        }
        public async Task<ResponseObject<DataResponseUser>> ChangePassword(long userId, Request_ChangePassword request)
        {
            try
            {
                var user = await _baseUserRepository.GetByIdAsync(userId); //Không cần check tồn tại hay không vì lấy thông tin của tk trong phiên đăng nhập
                bool checkPass = BCryptNet.Verify(request.OldPassword, user.Password);
                if (!checkPass)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không chính xác",
                        Data = null,
                    };
                }
                if (!request.NewPassword.Equals(request.ConfirmPassword))
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không trùng khớp",
                        Data = null,
                    };
                }
                user.Password = BCryptNet.HashPassword(request.NewPassword);
                user.UpdateTime = DateTime.Now;
                await _baseUserRepository.UpdateAsync(user);
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đổi mật khẩu thành công",
                    Data = _userConverter.EntityToDTO(user),
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null,
                };
            }
        }
        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                var user= await _userRepository.GetUserByEmail(email);
                if(user == null)
                {
                    return "Email không tồn tại trong hệ thống";
                }
                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    ConfirmCode = GenerateCodeActive(),
                    ExpiryTime = DateTime.Now.AddMinutes(5),
                    UserId = user.Id,
                    IsConfirmed = false,
                };
                confirmEmail = await _baseConfirmEmailRepository.CreateAsync(confirmEmail);
                var message = new EmailMessage(new string[] { user.Email }, "Nhận mã xác nhận tại đây", $"Mã xác nhận là: {confirmEmail.ConfirmCode}");
                var send = _emailService.SendEmail(message);
                return "Gửi mã xác nhận thành công! Vui lòng kiểm tra email";
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> ConfirmCreateNewPassword(Request_CreateNewPassword request)
        {
            try
            {
                var confirmEmail = await _baseConfirmEmailRepository.GetAsync(x=>x.ConfirmCode.Equals(request.ConfirmCode));
                if(confirmEmail == null)
                {
                    return "Mã xác nhận không hợp lệ";
                }
                if (confirmEmail.ExpiryTime < DateTime.Now)
                {
                    return "Mã xác nhận đã hết hạn";
                }
                if(!request.NewPassword.Equals(request.ConfirmPassword))
                {
                    return "Mật khẩu không trùng khớp";
                }
                var user = await _baseUserRepository.GetAsync(x => x.Id == confirmEmail.UserId);
                user.Password = BCryptNet.HashPassword(request.NewPassword);
                user.UpdateTime = DateTime.Now;
                await _baseUserRepository.UpdateAsync(user);
                return "Tạo mật khẩu mới thành công";
            }catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> AddRolesToUser(long userId, List<string> roles)
        {
            var currentUser = _contextAccessor.HttpContext.User; //Lấy thông tin dựa vào Token
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return "Người dùng chưa được xác thực";
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return "Bạn không có quyền thực hiện chức năng này";
                }
                var user = await _baseUserRepository.GetByIdAsync(userId);
                if(user == null)
                {
                    return "Không tìm thấy người dùng";
                }
                await _userRepository.AddRoleToUserAsync(user, roles);
                return "Thêm quyền cho người dùng thành công!";
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteRoles(long userId, List<string> roles)
        {
            var currentUser = _contextAccessor.HttpContext.User; //Lấy thông tin dựa vào Token
            try
            {
                if (!currentUser.Identity.IsAuthenticated)
                {
                    return "Người dùng chưa được xác thực";
                }
                if (!currentUser.IsInRole("Admin"))
                {
                    return "Bạn không có quyền thực hiện chức năng này";
                }
                var user = await _baseUserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return "Không tìm thấy người dùng";
                }
                await _userRepository.DeleteRoleAsync(user, roles);
                return "Xóa quyền người dùng thành công!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<ResponseObject<DataResponseUser>> GetUserById(long id)
        {
            var user = await _baseUserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return new ResponseObject<DataResponseUser>()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Người dùng không tồn tại",
                    Data = null,
                };
            }
            return new ResponseObject<DataResponseUser>
            {
                Status = StatusCodes.Status200OK,
                Message = "Lấy người dùng thành công",
                Data = _userConverter.EntityToDTO(user),
            };
        }

        #region Private Methods
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInHours"], out int tokenValidityInHours);
            var expiration = DateTime.Now .AddHours(tokenValidityInHours);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: expiration,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
        
        private string GenerateRefreshToken()
        {
            var randomNumber= new Byte[64];
            var range=RandomNumberGenerator.Create();
            range.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        


        #endregion

    }
}
