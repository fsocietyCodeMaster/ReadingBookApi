using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReadingBookApi.Context;
using ReadingBookApi.Customized;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/ResponseInformation.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<BookDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

builder.Services.AddIdentity<CustomUser, IdentityRole>(c =>
{
    c.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<BookDbContext>();


builder.Services.AddCustomServices();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Host.UseSerilog();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidAudience = builder.Configuration["Jwt:Audience"],
    };
});

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
