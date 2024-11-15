using classwork.Core.Interfaces;
using classwork.Storage;
using classwork_15._11._24;
using classwork_15._11._24.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Local")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value ?? string.Empty)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddScoped<ISourceService, SourceService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers(opt => opt.Filters.Add(new ExceptionFilter()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
