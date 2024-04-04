using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using SOILSTREAMAPI.Data;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Services.Implementations;
using SOILSTREAMAPI.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true)
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling =
                     Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(x =>
                     x.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling =
    Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(x =>
    x.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddDbContextPool<SoilStreamsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value))
    };
}).AddGoogle(options =>
{
    options.ClientId = builder.Configuration.GetSection("GoogleOAUTH")["ClientId"];
    options.ClientSecret = builder.Configuration.GetSection("GoogleOAUTH")["ClientSecret"];
}).AddCookie();
builder.Services.AddIdentity<User, IdentityRole>(opt =>
    {
        opt.SignIn.RequireConfirmedEmail = false;
    })
        .AddEntityFrameworkStores<SoilStreamsDbContext>()
        .AddDefaultTokenProviders();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IStoreProductServices, StoreProductServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IJWTService, JWTService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
