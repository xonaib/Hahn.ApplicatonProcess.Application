using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Domain.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.December2020.Model;
using Hahn.ApplicatonProcess.December2020.Web.Swagger;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Hahn.ApplicatonProcess.December2020.Web
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

            services.AddDbContext<DBContext>(opt => opt.UseInMemoryDatabase("Test"));
            //services.AddScoped()
            //services.AddScoped<ApplicantsRepository, ApplicantsRepository>();
            services.AddScoped<ApplicantsRepository, ApplicantsRepository>();

            services.AddMvc()
                .AddFluentValidation()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null); 
                //.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                //.AddNewtonsoftJson(options =>
                //{
                //    jsonSettings.ContractResolver = new JsonContractResolver();
                //});

            services.AddTransient<IValidator<Applicant>, ApplicantValidator>();

            //services.AddSwaggerExamplesFromAssemblyOf<ApplicantExample>();
            //services.AddSwaggerGen(c =>
            //{

            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "webAPI",
            //        Version = "v1",
            //        Description = "The API for FeedMe",
            //        Contact = new OpenApiContact
            //        {
            //            Name = "",
            //            Email = "",
            //        },
            //        License = new OpenApiLicense
            //        {
            //            Name = "Licence",
            //            Url = new Uri("https://example.com/license"),
            //        }
            //    });

            //    // [SwaggerRequestExample] & [SwaggerResponseExample]
            //    c.ExampleFilters();

            //    // Set the comments path for the Swagger JSON and UI.
            //    //var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    //var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    //c.IncludeXmlComments(xmlPath);
            //});

            //services.UseSwagger();
            //services.UseSwaggerUI(c =>
            //{
            //    c.RoutePrefix = "swagger/ui";
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI(v1)");
            //});

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title= "MyAPI", Version = "v1" });
            //    c.OperationFilter<ExamplesOperationFilter>();
            //    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            //    //    // [SwaggerRequestExample] & [SwaggerResponseExample]
            //    //    // version < 3.0 like this: c.OperationFilter<ExamplesOperationFilter>(); 
            //    //    // version 3.0 like this: c.AddSwaggerExamples(services.BuildServiceProvider());
            //    //    // version > 4.0 like this:
            //    //    c.ExampleFilters();

            //    //    c.OperationFilter<AddHeaderOperationFilter>("correlationId", "Correlation Id for the request", false); // adds any string you like to the request headers - in this case, a correlation id
            //    //    c.OperationFilter<AddResponseHeadersFilter>(); // [SwaggerResponseHeader]

            //    //    var filePath = Path.Combine(AppContext.BaseDirectory, "WebApi.xml");
            //    //    c.IncludeXmlComments(filePath); // standard Swashbuckle functionality, this needs to be before c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>()

            //    //    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>(); // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
            //    //                                                                  // or use the generic method, e.g. c.OperationFilter<AppendAuthorizeToSummaryOperationFilter<MyCustomAttribute>>();

            //    //    // add Security information to each operation for OAuth2
            //    //    c.OperationFilter<SecurityRequirementsOperationFilter>();
            //    //    // or use the generic method, e.g. c.OperationFilter<SecurityRequirementsOperationFilter<MyCustomAttribute>>();

            //    //    // if you're using the SecurityRequirementsOperationFilter, you also need to tell Swashbuckle you're using OAuth2
            //    //    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            //    //    {
            //    //        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
            //    //        In = ParameterLocation.Header,
            //    //        Name = "Authorization",
            //    //        Type = SecuritySchemeType.ApiKey
            //    //    });
            //});

            services.AddSwaggerExamples();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                c.ExampleFilters();
                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            
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


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
