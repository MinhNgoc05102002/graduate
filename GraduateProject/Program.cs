using GP.Business.IService;
using GP.Business.Service;
using GP.Common.Helpers;
using GP.DAL.IRepository;
using GP.DAL.Repository;
using GP.Models.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://example.com",
                                "http://localhost:5173").AllowAnyOrigin()
                                                         .AllowAnyMethod()
                                                         .AllowAnyHeader();
        });
});
builder.Services.AddControllers(
    //options =>
    //{
    //    options.Filters.Add(typeof(CustomExceptionFilter));
    //}
).AddNewtonsoftJson(options => 
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


builder.Services.AddDbContext<QuizletDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuizletDb"));
});

// Turn off default Validate
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});

// Add service
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICreditService, CreditService>();
builder.Services.AddScoped<IFolderService, FolderService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IFlashcardService, FlashcardService>();


// Add repository 
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICreditRepository, CreditRepository>();
builder.Services.AddScoped<IFolderRepository, FolderRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IFlashcardRepository, FlashcardRepository>();


// Add healper
builder.Services.AddScoped<MappingProfile>();
builder.Services.AddScoped<AuthHelper>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
