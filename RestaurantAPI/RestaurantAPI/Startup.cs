using DataAccess;
using DataAccess.DataTransferObjects;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestaurantAPI.Authentication;
using System.Text.Json.Serialization;

namespace RestaurantAPI
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
            services.AddDbContext<RestaurantContext>(options =>
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Configuration.Bind("JwtSettings", options));
            //Dependency inject repositories, so Controller constructors can be called from the web.
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IRepository<RestaurantTablesDTO>, TableRepository>();
            services.AddScoped<IRepository<CustomerDTO>, CustomerRepository>();
            services.AddScoped<IRepository<EmployeeDTO>, EmployeeRepository>();
            services.AddScoped<IRepository<ReservationDTO>, ReservationRepository>();
            services.AddScoped<IAccountRepository, UserRepository>();
            services.AddScoped<IAuthManager>(am =>
                new AuthManager(am.GetRequiredService<IAuthRepository>(),
                    Configuration["SigningKey"]));
            //Danger after this
            services.AddControllers().AddJsonOptions(opt =>
            {
                //[FromBody] does not support enum by default
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}