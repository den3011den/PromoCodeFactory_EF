using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.IO;

namespace PromoCodeFactory.WebHost
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddDbContext<ApplicationDbContext>(optionsBuilder
                => optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlite(configuration.GetConnectionString("ApplicationConnection")));

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddScoped(typeof(IRepository<Employee>), (x) =>
            //    new InMemoryRepository<Employee>(FakeDataFactory.Employees));
            //services.AddScoped(typeof(IRepository<Role>), (x) =>
            //    new InMemoryRepository<Role>(FakeDataFactory.Roles));
            //services.AddScoped(typeof(IRepository<Preference>), (x) =>
            //    new InMemoryRepository<Preference>(FakeDataFactory.Preferences));
            //services.AddScoped(typeof(IRepository<Customer>), (x) =>
            //    new InMemoryRepository<Customer>(FakeDataFactory.Customers));

            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory API Doc";
                options.Version = "1.0";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi(x =>
            {
                x.DocExpansion = "list";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}