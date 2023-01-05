using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MoviesAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clinicAPI;
using Microsoft.EntityFrameworkCore;
using clinicAPI.Helpers;
using clinicAPI.Filters;
using clinicAPI.APIBehavior;

namespace MoviesAPI
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
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                
                options.AddDefaultPolicy(builder =>
                {
                    var frontendURL = Configuration.GetValue<string>("frontend_url");
                    builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader()
                           .WithExposedHeaders(new string[] { "totalAmountOfRecords" });
                });
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IFileStorageService, AzureStorageService>();

            //local storage of the photo <- UNCOMMENT FOR LOCAL STORAGE IN WWWROOT OF THE PHOTO ->
            //services.AddScoped<IFileStorageService, InAppStorageService>();
            //services.AddHttpContextAccessor();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(MyExceptionFilter));
                options.Filters.Add(typeof(ParseBadRequest));
            }).ConfigureApiBehaviorOptions(BadRequestBehavior.Parse);
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "clinicAPI", Version = "v1" });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Map("/map1", (app) =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("nu merge pipeline");
                });
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CLINICAPI v1"));

            }

            app.UseHttpsRedirection();

            //local storage of the photo <- UNCOMMENT FOR LOCAL STORAGE IN WWWROOT OF THE PHOTO ->
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}