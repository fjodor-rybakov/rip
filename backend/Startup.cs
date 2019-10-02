using System;
using System.Text;
using backend.controllers;
using backend.core.configs;
using backend.core.connectors;
using backend.helper;
using backend.middleware;
using backend.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace backend
{
    public class Startup
    {
        private const string MyAllowSpecificOrigins = "AllowAll";
        private readonly ApiErrors _apiErrors;

        public Startup()
        {
            _apiErrors = new ApiErrors();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var authConfig = new DataAuthConfig()
            {
                Key = Environment.GetEnvironmentVariable("AUTH_KEY"),
                Audience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE"),
                Issuer = Environment.GetEnvironmentVariable("AUTH_ISSUER"),
                Lifetime = int.Parse(Environment.GetEnvironmentVariable("AUTH_LIFETIME") ??
                                     throw new Exception("AUTH_LIFETIME must me a number"))
            };
            services.AddDbContext<RipDatabase>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authConfig.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authConfig.Audience,
                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authConfig.Key)),
                        ValidateIssuerSigningKey = true
                    };
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest).AddNewtonsoftJson();
            services.AddControllers();
            services.AddSingleton(sp => new ApiErrors());
            services.AddScoped(sp => new AuthService(new RipDatabase(), authConfig, sp.GetService<ApiErrors>()));
            services.AddScoped(sp => new AuthController(sp.GetService<AuthService>(), sp.GetService<ApiErrors>()));

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins(Environment.GetEnvironmentVariable("FRONTEND_ADDRESS")).AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(errorApp =>
                AppHandlerExceptionMiddleware.AppHandlerException(errorApp, _apiErrors));

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
                endpoints.MapControllers();
            });

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 401)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(_apiErrors.InvalidToken));
                }

                if (context.Response.StatusCode == 403)
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(_apiErrors.AccessDenied));
                }
            });
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
        }
    }
}