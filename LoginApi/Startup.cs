using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using IdentityServer4.Test;
using LoginApi.Configuration;
using LoginApi.Models;
using LoginApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoginApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    //fix this to use configurationmanager setting, because we will have dev/staging/prod environments
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddControllers()
            // Below JSON options are the default for ASP NET CORE 3.1
            // Written only for clarity. Can be safely removed as a whole.
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                options.JsonSerializerOptions.AllowTrailingCommas = false;
            });

            //services.AddDbContext<CatalogContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityServer()
                .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
                .AddTestUsers(new UserRepository().GetUsers())
                .AddInMemoryClients(InMemoryConfig.GetClients())
                .AddDeveloperSigningCredential();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
           // The method below also adds IHttpClientFactory to the service collection.
           // https://github.com/IdentityModel/IdentityModel.AspNetCore.OAuth2Introspection/blob/master/src/OAuth2IntrospectionExtensions.cs#L53
           .AddIdentityServerAuthentication(options =>
           {
               options.Authority = "https://localhost:44337/api/identity";
               options.ApiName = "LoginApi";
               options.ApiSecret = "testSecret";
               options.SupportedTokens = SupportedTokens.Jwt;
               options.RequireHttpsMetadata = false;
               options.Validate();
           });

            services.AddHttpContextAccessor();

            DependencyConfig.Populate(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.Map("/api/identity", identityServerApp => identityServerApp.UseIdentityServer());

            app.UseRouting();

            app.UseCors(builder =>
                builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());

            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
