using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CleverAPI.Controllers;
using CleverAPI.Data;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CleverAPI
{
    public class Startup
    {
        private static CATOController CATO;
        private static CompaniesKKController CompaniesKK;
        private static ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            //services
            //    .AddTransient<Data.ApplicationDbContext, Data.ApplicationDbContext>();

            _context = services.BuildServiceProvider()
                .GetService<ApplicationDbContext>();

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Clever API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMvcCore()
                .AddApiExplorer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                options => options.AllowAnyOrigin()
            );

            //app.UseSwagger();

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});

            app.UseMvc();

            CATO = new CATOController(_context);
            RecurringJob.AddOrUpdate(
                () => CATO.ParseCATO(Configuration.GetSection("eGovCATODownloadDefaultUrl").Get<string>()),
                "0 2 * * *",
                TimeZoneInfo.Local);

            CompaniesKK = new CompaniesKKController(_context, _hostingEnvironment);
            RecurringJob.AddOrUpdate(
                () => CompaniesKK.ParseCompaniesKK(Configuration.GetSection("eGovCompaniesDownloadUrl").Get<string>()),
                "0 2 * * 0",
                TimeZoneInfo.Local);
        }
    }
}
