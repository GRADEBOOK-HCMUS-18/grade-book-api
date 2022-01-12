using System;
using System.Text;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using CloudinaryDotNet;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace grade_book_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(
                builder =>
                    builder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
                 .LogTo(Console.WriteLine, LogLevel.Information)
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "grade_book_api", Version = "v1"});
            });
            services.AddCors();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new
                        SymmetricSecurityKey(Encoding.ASCII.GetBytes("3aacfb02-b67b-4923-8a2d-21a103902b91")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization();

            // set up DI services
            ConfigureIocContainer(services);


        }

        private void ConfigureIocContainer(IServiceCollection services)
        {
            // set up cloudinary
            var cloud = Configuration.GetValue<string>("AccountSettings:CloudName");
            var apiKey = Configuration.GetValue<string>("AccountSettings:ApiKey");
            var apiSecret = Configuration.GetValue<string>("AccountSettings:ApiSecret");
            var cloudinaryAccount = new Account(cloud, apiKey, apiSecret);

            var cloudinary = new Cloudinary(cloudinaryAccount);
            services.AddScoped<IUserJwtAuthService, UserJwtAuthService>();
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICloudPhotoHandler, CloudinaryPhotoHandlerAdapter>();
            services.AddScoped<IInvitationService, InvitationService>();
            services.AddScoped<IEmailSender, MailKitEmailSenderAdapter>();
            services.AddSingleton(Configuration);
            services.AddSingleton(cloudinary);
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IAdminService, AdminService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "grade_book_api v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(_ => true) // allow any origin
                .AllowCredentials()); // allow credentials 
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}