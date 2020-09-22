using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project_ClicLikes.Models;

namespace Project_ClicLikes
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

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("Project_DB").ToString();

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            //services.AddCors(options => options.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod()));
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
