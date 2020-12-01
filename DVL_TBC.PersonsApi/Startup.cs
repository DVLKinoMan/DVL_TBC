using DVL_TBC.Domain.Abstract;
using DVL_TBC.Domain.Concrete;
using DVL_TBC.Domain.Models;
using DVL_TBC.PersonsApi.Extensions;
using DVL_TBC.PersonsApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace DVL_TBC.PersonsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PersonsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PersonsContext")));

            services
                .AddControllers(options => options.Filters.Add(typeof(PersonActionFilter)))
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddScoped<IPersonsRepository, PersonsRepository>();
            services.AddScoped<IRelatedPersonsRepository, RelatedPersonsRepository>();
            services.AddScoped<IPhonesRepository, PhonesRepository>();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Handling unhandled exceptions
            app.UseUnhandledExceptionHandler();

            //Setting language for application
            app.UseAcceptLanguageHeader();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DVL_TBC");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });
        }
    }
}
