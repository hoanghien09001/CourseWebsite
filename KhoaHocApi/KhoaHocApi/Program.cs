using KhoaHocApi.Application.Constant;
using KhoaHocApi.Application.Handle.HandleEmail;
using KhoaHocApi.Application.ImplementService;
using KhoaHocApi.Application.InterfaceService;
using KhoaHocApi.Application.Payloads.Mapper;
using KhoaHocApi.Application.Payloads.Mapper.BillsConverter;
using KhoaHocApi.Application.Payloads.Mapper.PracticeConverter;
using KhoaHocApi.Application.Payloads.Mapper.StudentConverter;
using KhoaHocApi.Application.Payloads.Mapper.StudyConverter;
using KhoaHocApi.Application.Payloads.Mapper.SubjectConverter;
using KhoaHocApi.Domain.Entities;
using KhoaHocApi.Domain.InterfaceRepositories;
using KhoaHocApi.Infrastructure.DataContexts;
using KhoaHocApi.Infrastructure.ImplementRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(Constant.AppSettingKeys.DEFAULT_CONNECTION)));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDbContext, ApplicationDbContext>();
builder.Services.AddScoped<UserConverter>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IBaseRepository<ConfirmEmail>, BaseRepository<ConfirmEmail>>();
builder.Services.AddScoped<IBaseRepository<RefreshToken>, BaseRepository<RefreshToken>>();
builder.Services.AddScoped<IBaseRepository<Permission>, BaseRepository<Permission>>();
builder.Services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();

builder.Services.AddScoped<IBaseRepository<SubjectDetail>, BaseRepository<SubjectDetail>>();
builder.Services.AddScoped<IBaseRepository<Blog>, BaseRepository<Blog>>();
builder.Services.AddScoped<IBaseRepository<LikeBlog>, BaseRepository<LikeBlog>>();
builder.Services.AddScoped<IBaseRepository<CommentBlog>, BaseRepository<CommentBlog>>();
builder.Services.AddScoped<IBaseRepository<BillStatus>, BaseRepository<BillStatus>>();
builder.Services.AddScoped<IBaseRepository<MakeQuestion>, BaseRepository<MakeQuestion>>();
builder.Services.AddScoped<IBaseRepository<Answers>, BaseRepository<Answers>>();


builder.Services.AddScoped<IBaseRepository<Bill>, BaseRepository<Bill>>();
builder.Services.AddScoped<IBaseRepository<Course>, BaseRepository<Course>>();



builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IVNPayService, VNPayService>();

builder.Services.AddScoped<BlogConverter>();
builder.Services.AddScoped<CommentConverter>();
builder.Services.AddScoped<MakeQuestionConerter>();
builder.Services.AddScoped<AnswerConverter>();
builder.Services.AddScoped<BillConverter>();
builder.Services.AddScoped<LikeBlogConverter>();
//Dohomework
builder.Services.AddScoped<IBaseRepository<DoHomework>, BaseRepository<DoHomework>>();
builder.Services.AddScoped<DohomeworkConverter>();
builder.Services.AddScoped<IBaseRepository<RegisterStudy>, BaseRepository<RegisterStudy>>();
//Practice
builder.Services.AddScoped<IBaseRepository<ProgramingLanguage>, BaseRepository<ProgramingLanguage>>();
builder.Services.AddScoped<IBaseRepository<SubjectDetail>, BaseRepository<SubjectDetail>>();
builder.Services.AddScoped<IBaseRepository<Practice>, BaseRepository<Practice>>();
builder.Services.AddScoped<IPractiveService, PracticeService>();
builder.Services.AddScoped<PractiveConverter>();

//CertificateService
builder.Services.AddScoped<ICertifiacateService, CertificateService>();
builder.Services.AddScoped<IBaseRepository<CertificateType>, BaseRepository<CertificateType>>();
builder.Services.AddScoped<IBaseRepository<Certificate>, BaseRepository<Certificate>>();
builder.Services.AddScoped<CertificateConverter>();
builder.Services.AddScoped<CertificateTypeConverter>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<TeacherConverter>();


//Course
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IBaseRepository<Course>, BaseRepository<Course>>();
builder.Services.AddScoped<CourseConverter>();
builder.Services.AddHttpContextAccessor();

//Subject
builder.Services.AddScoped<IBaseRepository<Subject>, BaseRepository<Subject>>();
builder.Services.AddScoped<IBaseRepository<SubjectDetail>, BaseRepository<SubjectDetail>>();
builder.Services.AddScoped<IBaseRepository<CourseSubject>, BaseRepository<CourseSubject>>();
builder.Services.AddScoped<SubjectConverter>();
builder.Services.AddScoped<SubjectDetailsConverter>();
builder.Services.AddScoped<CourseSubjectConverter>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
//Study
builder.Services.AddScoped<IBaseRepository<RegisterStudy>, BaseRepository<RegisterStudy>>();
builder.Services.AddScoped<StudyConverter>();
builder.Services.AddScoped<IBaseRepository<LearningProgress>, BaseRepository<LearningProgress>>();
builder.Services.AddScoped<LearningProcessConverter>();
builder.Services.AddScoped<IStudyService, StudyService>();

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Auth Api", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Vui lòng nh?p Token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[] {}
        }
    });
});   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:8080"));

app.UseAuthorization();

app.MapControllers();

app.Run();
