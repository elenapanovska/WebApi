using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Homework.WebApi.ToDoApp.Services;
using Homework.WebApi.ToDoApp.Services.Helpers;
using Homework.WebApi.ToDoApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Homework.WebApi.ToDoApp.Api
{
    public class Startup
    {
        private readonly string _localhost = "http://localhost:56115";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            DIModule.RegisterModule(services, connectionString);

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IToDoService, ToDoService>();

            //automapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);


            //token authorisation
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = _localhost,
                    //ValidAudience = _localhost,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("logInSecretKey@123456"))
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
