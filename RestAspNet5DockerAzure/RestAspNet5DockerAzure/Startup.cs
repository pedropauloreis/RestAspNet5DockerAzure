using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestAspNet5DockerAzure.Model.Context;
using RestAspNet5DockerAzure.Business;
using RestAspNet5DockerAzure.Business.Implementations;
using Serilog;
using System.Collections.Generic;
using RestAspNet5DockerAzure.Repository.Generic;
using Microsoft.Net.Http.Headers;
using RestAspNet5DockerAzure.Hypermedia.Filters;
using RestAspNet5DockerAzure.Hypermedia.Enricher;
using Microsoft.AspNetCore.Rewrite;
using RestAspNet5DockerAzure.Repository;
using RestAspNet5DockerAzure.Repository.Implementations;
using RestAspNet5DockerAzure.TokenService;
using RestAspNet5DockerAzure.TokenService.Implementations;
using RestAspNet5DockerAzure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RestAspNet5DockerAzure
{
    public class Startup
    {

        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TOKEN CONFIGURATIONS
            var tokenConfigurations = new TokenConfiguration();
            var uploadConfigurations = new UploadConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);
            new ConfigureFromConfigurationOptions<UploadConfiguration>(Configuration.GetSection("UploadConfigurations")).Configure(uploadConfigurations);

            services.AddSingleton(tokenConfigurations);
            services.AddSingleton(uploadConfigurations);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidAudience = tokenConfigurations.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });

            

            //CORS Support
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                }
                );
            }

            );

            services.AddControllers();

            //Database Context
            var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            //Migration and DataSet Startup Support
            //if (Environment.IsDevelopment())
            //{
            //    MigrateDatabase(connection);
            //}

            //Content Negotiation Support (Json and XML)
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            //HATEOAS Support
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new DepartmentEnricher());
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
            filterOptions.ContentResponseEnricherList.Add(new UserEnricher());
            services.AddSingleton(filterOptions);
            
            //Versioning Support
            services.AddApiVersioning();

            //Controllers Injection
            services.AddScoped<IDepartmentBusiness, DepartmentBusinessImplementation>();
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped<IUserBusiness, UserBusinessImplementation>();
            services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();

            //File Support Injection
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IFileBusiness, FileBusinessImplementation>();


            //Token Injection
            services.AddTransient<ITokenService, TokenServiceImplementation>();

            //Generic Repository Support
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            //Custom Repository Support
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Swagger Support
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "RestAspNet5DockerAzure", 
                    Version = "v1" ,
                    Description = "API RESTful Example",
                    Contact = new OpenApiContact
                    {
                        Name = "Pedro Paulo de Oliveira Reis",
                        Url = new Uri("https://github.com/pedropauloreis"),
                        
                    }
                });
            });
            



        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestAspNet5DockerAzure v1 - Azure View"));

                //Redirect "/" root URL to "/swagger"
                var option = new RewriteOptions();
                option.AddRedirect("^$", "swagger");
                app.UseRewriter(option);
                

            app.UseHttpsRedirection();

            app.UseRouting();

            //CORS Support
            //Must be included after UseHttpsRedirection() and UseRouting(), and before UseEndpoints()
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //HETEOAS LinkBuilder Support
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");
            });
        }

        //Execution of Migration and DataSet Seeders on Startup
        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<String> { "db/migrations","db/dataset" },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();

            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}
