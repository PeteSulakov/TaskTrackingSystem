using BLL.Interfaces;
using BLL.Services;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Infrastructure;

namespace WebApi.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder =>
				builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());
			});
		}

		public static void ConfigureIISIntegration(this IServiceCollection services) =>
			services.Configure<IISOptions>(options => { });

		public static void ConfigureLoggerService(this IServiceCollection services) =>
			services.AddScoped<ILoggerManager, LoggerManager>();

		public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
			services.AddDbContext<TaskDbContext>(opts =>
				opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

		public static void ConfigureIdentity(this IServiceCollection services)
		{
			var builder = services.AddIdentity<User, IdentityRole>(o =>
			{
				o.Password.RequireDigit = true;
				o.Password.RequireLowercase = false;
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequiredLength = 10;
				o.User.RequireUniqueEmail = true;
			});

			builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
				builder.Services);
			builder.AddEntityFrameworkStores<TaskDbContext>().AddDefaultTokenProviders();
		}

		public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection("JwtSettings");
			var secretKey = Environment.GetEnvironmentVariable("SECRET");

			services.AddAuthentication(opt => {
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
					ValidAudience = jwtSettings.GetSection("validAudience").Value,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
				};
			});
		}

		public static void ConfigureUnitOfWork(this IServiceCollection services) =>
			services.AddScoped<IUnitOfWork, UnitOfWork>();

		public static void ConfigureBLL(this IServiceCollection services)
		{
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<ITaskService, TaskService>();
			services.AddScoped<IProjectService, ProjectService>();
			services.AddScoped<IStatusService, StatusService>();
			services.AddScoped<IStatisticService, StatisticService>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IAdminService, AdminService>();
		}
	}
}
