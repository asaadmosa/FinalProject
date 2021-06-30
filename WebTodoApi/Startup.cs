using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Todos.Contracts.Repositories;
using Todos.DataAccess;
using Todos.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Todos.WebTodoApi
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
            services.AddDbContext<TodosDataContext>(options=>options.UseSqlServer("name=ConnectionStrings:todo"));
            services.AddControllers().AddJsonOptions(x =>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            //is responsible of the services resolve the dependancy injection (by the interface )
            //like the configuration of the whole system
            services.AddScoped<ITodosRepository, SqlTodosRepository>();
            services.AddCors(options =>
            {
                options.AddPolicy("Policy1",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200");
                        builder.WithHeaders("content-type");
                        builder.WithMethods("PUT", "DELETE", "GET", "POST","OPTIONS");
                        builder.AllowCredentials();
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {//this middleware will handle the http errors exception
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("Policy1");



            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
