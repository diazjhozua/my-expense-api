using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using my_expense_api.Components.Handlers;
using my_expense_api.Components.Handlers.Others;
using my_expense_api.Data;
using my_expense_api.Services.AnalyticsService;
using my_expense_api.Services.CategoryService;
using my_expense_api.Services.ExpenseService;
using Npgsql;

namespace my_expense_api
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
            services.AddDbContext<DataContext>(x => x.UseNpgsql(Environment.GetEnvironmentVariable("DefaultConnection")));
            
            services.AddControllers();

            ConfigureSwagger(services);

            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder.WithOrigins(Environment.GetEnvironmentVariable("AllowedHost")).AllowAnyHeader().AllowAnyMethod()
            ));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Environment.GetEnvironmentVariable("Token"))),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                };
            });
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IHandler, Handler>();
            services.AddScoped<IUtilityService, UtilityService>();
                        
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
        }

        private static void ConfigureSwagger(IServiceCollection services) 
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo() { Title = "My Expense API", Version = "v1", Description = "Manage and track your expense online",         
                Contact = new OpenApiContact()
                {
                    Name = "My Expense Web",
                    Url = new Uri(Environment.GetEnvironmentVariable("AllowedHost"))
                }});

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
            
                config.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                config.AddSecurityRequirement(securityRequirement);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.RoutePrefix = "";
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "My Expense API");
                config.DefaultModelsExpandDepth(-1);
            });

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            
            app.UseAuthorization();
                
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            AppDbInitializer.Seed(app, env);
        }
    }
}
