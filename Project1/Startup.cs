using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project1.DAL;
using Project1.DAL.Model;
using Project1.DAL.Repo;
using Project1.DAL.Repos;
using Project1.Services;
using Project1.Services.Interface;
using Serilog;
using System.IO;
using Newtonsoft;
using System;
using Elastic.Apm.NetCoreAll;

namespace Project1
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

            //string appRoot = Directory.GetCurrentDirectory();
            //string path = Path.Combine(appRoot, "App_Data");
            var dbName = $"UserDb_{DateTime.Now.ToFileTimeUtc()}";
            services.AddDbContext<TaskManagementContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection").Replace("[DataDirectory]", path));
                options.UseInMemoryDatabase(dbName);
                options.EnableDetailedErrors();
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IUserTaskService), typeof(UserTaskService));
            services.AddScoped(typeof(IUserTaskStatusService), typeof(UserTaskStatusService));
            

            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddSwaggerGen();
            
            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAllElasticApm(Configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseSerilogRequestLogging();

            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                TaskManagementContext context = serviceScope.ServiceProvider.GetRequiredService<TaskManagementContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                Seed(context);
              

            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OData 8.x OpenAPI");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private void Seed(TaskManagementContext context)
        {
            context.UserTaskStatuses.AddRange(new[] { new UserTaskStatus { Name = "Pending", Color = "green" }, new UserTaskStatus { Name = "In Progress", Color = "yellow" }, new UserTaskStatus { Name = "Complated", Color = "blue" }, new UserTaskStatus { Name = "Cancel", Color = "red" } });
            context.SaveChanges();
            context.Users.AddRange(new[] { new User { Name = "User1", Email="user1@gmail.com",Surname= "Paduvan1" }, new User { Name = "User2", Email = "user2@gmail.com", Surname = "Paduvan2" } , new User { Name = "User3", Email = "user3@gmail.com", Surname = "Paduvan3" } });
            context.SaveChanges();

            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(1), StartDate = DateTime.Now.Date, Subject = "include test", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(1), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(1), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });


            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(2), StartDate = DateTime.Now.Date, Subject = "include test", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(2), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 4 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(2), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });

            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(3), StartDate = DateTime.Now.Date, Subject = "include test", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(3), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 2 });
            context.UserTasks.Add(new UserTask { UserId = 1, StartTime = TimeSpan.FromHours(3), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });

            context.UserTasks.Add(new UserTask { UserId = 2, StartTime = TimeSpan.FromHours(4), StartDate = DateTime.Now.Date, Subject = "include test", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 2, StartTime = TimeSpan.FromHours(4), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 2, StartTime = TimeSpan.FromHours(4), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 1 });

            context.UserTasks.Add(new UserTask { UserId = 3, StartTime = TimeSpan.FromHours(5), StartDate = DateTime.Now.Date, Subject = "include test", UserTaskStatusId = 1 });
            context.UserTasks.Add(new UserTask { UserId = 3, StartTime = TimeSpan.FromHours(5), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 3 });
            context.UserTasks.Add(new UserTask { UserId = 3, StartTime = TimeSpan.FromHours(5), StartDate = DateTime.Now.Date, Subject = "include test1", UserTaskStatusId = 5 });

            context.SaveChanges();
        }
    }
}
