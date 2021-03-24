using AutoMapper;
using CC.Minesweeper.Api.Middlewares;
using CC.Minesweeper.Core.Domain.Services;
using CC.Minesweeper.Core.Interfaces;
using CC.Minesweeper.Core.Services;
using CC.Minesweeper.Infrastructure.Configurations;
using CC.Minesweeper.Infrastructure.Repositories.Repositories;
using CC.Minesweeper.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace CC.Minesweeper.Api
{
    public class Startup
    {
        private readonly MongoDbConfiguration mongoDbConfiguration;
        private readonly SecurityConfiguration securityConfiguration;

        private const string allowedOrigins = "_allowedOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            mongoDbConfiguration = configuration.GetSection(nameof(MongoDbConfiguration)).Get<MongoDbConfiguration>();
            securityConfiguration = configuration.GetSection(nameof(SecurityConfiguration)).Get<SecurityConfiguration>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.EnableEndpointRouting = false;

            })
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options =>
            {
                options.AddPolicy(allowedOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.WithExposedHeaders("Content-Disposition");
                });
            });

            services
                .AddSingleton(mongoDbConfiguration)
                .AddSingleton(securityConfiguration)
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddSingleton<UsersService>()
                .AddSingleton<IUsersRepository, UsersRepository>()
                .AddSingleton<IGameRepository, GameRepository>()
                .AddSingleton<IEncryptionSerice, EncryptionService>()
                .AddSingleton<IValidatorFactory, ServiceProviderValidatorFactory>();

            services.AddControllers().AddNewtonsoftJson();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityConfiguration.Secret))
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Minesweeper Api", Version = "v1.0" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCors(allowedOrigins);

            app.UseAuthorization();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1.0/swagger.json", "Minesweeper Api v1.0");
            });
        }
    }
}
