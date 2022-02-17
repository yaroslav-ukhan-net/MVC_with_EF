using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Models;
using Models.Models;
using Services;
using DataAccess.EF;

namespace WebApi
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
            var connectionString = (@"Data Source=DESKTOP-G7US54D\SQLEXPRESS;Initial Catalog=University;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            //my service 
            //services.AddScoped<IRepository<HomeTaskAssessment>>(p => new HomeTaskAssessmentRepository(connectionString));
            //services.AddScoped<IRepository<Student>>(p => new StudentRepository(connectionString));
            //services.AddScoped<IRepository<Course>>(p => new CourseRepository(connectionString));
            //services.AddScoped<IRepository<HomeTask>>(p => new HomeTaskRepository(connectionString));
            services.AddScoped<IRepository<Student>>(p => new RepositoryOptions<Student>(connectionString,new UniversityContext()));
            services.AddScoped<IRepository<Course>>(p => new RepositoryOptions<Course>(connectionString, new UniversityContext()));
            services.AddScoped<IRepository<HomeTask>>(p => new RepositoryOptions<HomeTask>(connectionString, new UniversityContext()));
            services.AddScoped<IRepository<HomeTaskAssessment>>(p => new RepositoryOptions<HomeTaskAssessment>(connectionString, new UniversityContext()));
            services.AddScoped<StudentService>();
            services.AddScoped<HomeTaskService>();
            services.AddScoped<CourseService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("defoult","{controller}/{action}/{id?}");
            });
        }
    }
}