using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WAC.Admin.Domain.Interfaces.Repositories;
using WAC.infra.Data.Contexto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters;
using FWK.ApiServices.Filters;
using WAC.Admin.Domain;
using FWK.Domain;
using AutoMapper.EquivalencyExpression;
using AutoMapper.EntityFramework;
using FWK.Domain.Extensions;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using FWK.AppService;
using System.IO;
using FWK.AppService.Interface;
using Microsoft.EntityFrameworkCore.Design;
using FWK.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using FWK.Caching;
using WAC.WebService.Admin.Shared; 
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using WAC.Domain.Interfaces.Services;
using WAC.Domain.Interfaces.Repositories;
using WAC.Infra.Data.Repositories;
using WAC.Domain.Services;

namespace WAC.WebService.Admin
{
    public class Startup
    {
        IHostingEnvironment environment;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            environment = env;


        }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var entorno = Configuration.GetValue<string>("Entorno");

            //services.AddDbContext<WACContext>(options =>
            //   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<WACContext>(opt =>
                opt.UseInMemoryDatabase("WAC")); 



            services.AddSwaggerGen(c =>
            {

                c.DescribeAllEnumsAsStrings();

                c.SwaggerDoc("v1", new Info { Title = "Admin (" + entorno + ")", Version = "v1" });

                var security = new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", new string[] { } }
                };

                // Define the BearerAuth scheme that's in use
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);
                // Assign scope requirements to operations based on AuthorizeAttribute



            });





            var identityUrl = Configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = identityUrl,
                    ValidAudience = identityUrl,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryVerySecretKey"))
                };
            });


            services.AddCors(

               options =>
               {
                   options.AddPolicy("AllowAllOrigins",
                   builder =>
                   {
                       builder
                       //.WithOrigins(_appConfiguration["App:CorsOrigins"].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o => o.RemovePostFix("/")).ToArray())
                       .AllowAnyOrigin() //TODO: Will be replaced by above when Microsoft releases microsoft.aspnetcore.cors 2.0 - https://github.com/aspnet/CORS/pull/94
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                   });
               }
               );

            services.AddCors();



            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.AddProfile<MappingProfile>();

            });



            services.AddMemoryCache();

            services.AddMvc(
                options =>
                {
                    options.Filters.Add(new HttpGlobalExceptionFilter(environment));
                })

                .AddRazorPagesOptions(options =>
                {

                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                })
                .AddJsonOptions(options =>

                    options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                    );


         

            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

            //Data Base
            services.AddScoped<IWACCContext>(provider => provider.GetService<WACContext>());


            services.AddTransient<WAC.Admin.Domain.Url.IAppUrlService, AngularAppUrlService>();
            services.AddTransient<WAC.Admin.Domain.Url.IWebUrlService, WebUrlService>();

            //Usuario
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IRandomUserService, RandomUserService>(); 

            services.AddTransient<IAppInitializeService, AppInitializeService>();


            services.AddTransient<FWK.Domain.Interfaces.Services.IAuthService, AuthService>();

            services.AddSingleton<FWK.Caching.Configuration.ICachingConfiguration, FWK.Caching.Configuration.CachingConfiguration>();
            services.AddSingleton<ICacheManager, FWK.Caching.Memory.MemoryCacheManager>();
             
            ///////////LOG///////////////
            if (environment.IsDevelopment())
            {

            }
            else
            {

            }


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp)
        { 

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();


            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });



            var pathBase = Configuration["APPL_PATH"];
            if (pathBase == "/")
                pathBase = "";
            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "WAC API V1");
            });

            //anti Pattern
            ServiceProviderResolver.ServiceProvider = app.ApplicationServices;


            var service = (IAppInitializeService)svp.GetService(typeof(IAppInitializeService));


            service.InitializeUser(); 
        }
    }
     
}
