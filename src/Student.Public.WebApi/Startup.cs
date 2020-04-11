using System;
using System.Data;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Student.DB.Migrations;
using Student.Public.WebApi.Extensions;
using Student.Public.WebApi.Filters;
using Student.Public.WebApi.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Query.Core.FluentExtensions;
using Student.Public.Infrastructure.Authentication;

namespace Student.Public.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //add dependencies here
            services.AddMemoryCache();
            services.AddLogging();

            services.AddScoped<AuthorizationApiFilter>();

            services
                .AddMvc(options => { options.Filters.Add(typeof(ErrorHandlingFilter)); })
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            #region Authentication
            services.Configure<JwtAuthOptions>(Configuration.GetSection("Auth:UserJwt"));
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Auth:UserJwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Auth:UserJwt:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:UserJwt:SecretKey"])),
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddAuthorization(o =>
            {
                var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser();
                o.DefaultPolicy = policy.Build();
            });

            services.AddScoped<Domain.Authentication.IAccessTokenFactory, JwtAccessTokenFactory>();
            services.AddScoped<Domain.Authentication.IPasswordHasher, PasswordHasher>();
            services.AddScoped<Domain.Authentication.IUserGetter, Infrastructure.Authentication.UserGetter>();
            services.AddScoped<Domain.Authentication.IRefreshTokenStore, RefreshTokenStore>();
            services.AddScoped<Domain.Authentication.UserAuthenticationService>();
            services.AddDbContext<Infrastructure.Authentication.AuthenticationDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Student"));
            });
            services.AddDbContext<RefreshTokenStoreDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Student"));
            });

            #endregion


            #region User

            services.AddScoped<Domain.Users.IUserRepository, Infrastructure.Users.UserRepository>();
            services.AddScoped<Domain.Users.IPasswordHasher, PasswordHasher>();
            services.AddScoped<Domain.Users.UserRegistrar>();
            services.AddDbContext<Infrastructure.Users.UserDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Student"));
            });

            #endregion

            services.AddScoped<IDbConnection, MySqlConnection>(_ => new MySqlConnection(Configuration.GetConnectionString("Student")));

            services.RegQueryProcessor(registry => { });


            #region Включение миграции в проект

            services.AddDbContext<MigrateDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("StudentMigration")));
            services.AddHostedService<MigrateService<MigrateDbContext>>();

            #endregion

            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "Student Api v1"); });
            app.UseRewriter(new RewriteOptions().AddRedirect(@"^$", "swagger", (Int32) HttpStatusCode.Redirect));
        }
    }
}