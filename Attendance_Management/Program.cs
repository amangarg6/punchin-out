using Attendance_Management;
using Attendance_Management.Data;
using Attendance_Management.Models.ViewModel;
using Attendance_Management.Repository;
using Attendance_Management.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string cs = builder.Configuration.GetConnectionString("Cons");
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>
 (option => option.UseSqlServer(cs, b =>
 b.MigrationsAssembly("Attendance_Management")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserrepo,Userrepo>();
builder.Services.AddScoped<IPunchrepo,Punchrepo>();
builder.Services.AddScoped<IStatusrepo,Statusrepo>();
builder.Services.AddScoped<ILeaverepo,Leaverepo>();
//identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
   .AddDefaultTokenProviders();


//jwt authentication
var appSettingSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingSection);
var appSetting = appSettingSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSetting.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
//***
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
