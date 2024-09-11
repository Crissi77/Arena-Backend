using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SportsArena.Database.SportsDb;
using SportsArena.Email.EmailModels;
using SportsArena.Email.EmailServices;
using SportsArena.Repository.Repositories.Class;
using SportsArena.Repository.Repositories.Class.User;
using SportsArena.Repository.Repositories.Interface;
using SportsArena.Repository.Repositories.Interface.IUser;
using SportsArena.Services.Services.Class;
using SportsArena.Services.Services.Class.User;
using SportsArena.Services.Services.Interface;
using SportsArena.Services.Services.Interface.IUser;
using SportsArenaWeb;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

//cors policy
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "jwtToken_Auth_API ",
        Version = "v1",
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme ="Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Here enter JWT Token with bearer format like bearer[space] token",

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


var connectionString = builder.Configuration.GetConnectionString("ArenaConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString,

    sqlOptions =>
    {
        sqlOptions.CommandTimeout(180); // Set the command timeout to 180 seconds
    }));

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });
//Jwt configuration ends here


builder.Services.AddTransient<IUserRegisterationRep, UserRegisterationRep>();
builder.Services.AddTransient<IUserRegisterationSer, UserRegisterationSer>();

builder.Services.AddTransient<IAdminRep, AdminRep>();
builder.Services.AddTransient<IAdminSer, AdminSer>();

builder.Services.AddTransient<IuserRep, UserRep>();
builder.Services.AddTransient<IuserSer, UserSer>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<ISendMailService, MailServices>();


// Bind BaseImageUrl to configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
