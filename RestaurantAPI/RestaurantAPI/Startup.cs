using System.Text;
using System.Text.Json.Serialization;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestaurantAPI.Authentication;
using Swashbuckle.AspNetCore.Filters;

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
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(auth =>
            {
                auth.RequireHttpsMetadata = false;
                auth.SaveToken = true;
                auth.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["SigningKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? new string[0];
            services.AddCors(options =>
            {
                //options.AddPolicy("mvcLoginPolicy",
                //    builder => builder.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod());
                //options.AddPolicy("PublicApi1",
                //    builder => builder.AllowAnyOrigin().WithMethods("Post").WithExposedHeaders("Content-Type")); 
                options.AddPolicy("PublicApi",
                    builder => builder.WithOrigins("https://localhost:44325").WithMethods("Get","Post"));
                options.AddPolicy("MyAllowJWTCredentialsPolicy",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44325/")
                            .AllowCredentials();
                    });
            });

            //Dependency inject repositories, so Controller constructors can be called from the web.
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IRepository<CustomerDTO>, CustomerRepository>();
            services.AddScoped<IRepository<EmployeeDTO>, EmployeeRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IRepository<UserDTO>, UserRepository>();
            services.AddScoped<IRepository<FoodDTO>, FoodRepository>();
            services.AddScoped<IRepository<OrderDTO>, OrderRepository>();
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
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "RestaurantAPI", Version = "v1"});
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
 
                c.OperationFilter<SecurityRequirementsOperationFilter>();
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
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}