using System.Text;
using System.Text.Json.Serialization;
using MarathonApp.BLL.Policies;
using MarathonApp.BLL.Services;
using MarathonApp.DAL.EF;
using MarathonApp.DAL.Entities;
using MarathonApp.DAL.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mapster;
using MarathonApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MarathonContext>(s => s.UseSqlServer(builder.Configuration.GetConnectionString("MarathonContext")));
builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MarathonContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthorizationHandler, UserPolicyHandler>();
builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<IImagesService, ImagesService>();
builder.Services.AddTransient<IPartnerService, PartnerService>();
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterMapster();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "me",
            ValidAudience = "you",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AuthSettings:Key").Value)),
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("NewUserPolicy", policy =>
    {
        policy.Requirements.Add(new UserPolicy(true));
    });
});

builder.Services.AddCors();

builder.Services.AddSwaggerGen(x =>
{ 
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "MarathonApp", Version = "v1" });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
