using CreditoAutomotriz.Domain.Hosts;
using CreditoAutomotriz.Domain.Implementations;
using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Infrastructure.AppContextDB;
using CreditoAutomotriz.Repository.Implementation;
using CreditoAutomotriz.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
//using Microsoft.Extensions.Logging;
//using OpenTelemetry.Exporter;
//using OpenTelemetry.Instrumentation.AspNetCore;
//using OpenTelemetry.Resources;
//using OpenTelemetry.Trace;
//using OpenTelemetry.Logs;
//using Microsoft.OpenApi;
//using Microsoft.OpenApi.Models;

namespace CreditoAutomotriz.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            });

            //services.AddControllers().AddJsonOptions(opciones =>
            //    opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
            //);

            #region OPENTELEMETRY
            string otelServer = Environment.GetEnvironmentVariable("OTEL_SERVER");
            var serviceName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            services.AddHttpContextAccessor();
            //if (!String.IsNullOrEmpty(otelServer))
            //{
            //    services.AddOpenTelemetryTracing(builder =>
            //       builder
            //      .AddAspNetCoreInstrumentation()
            //      .AddHttpClientInstrumentation()
            //      .AddConsoleExporter()
            //      .AddZipkinExporter(options =>
            //      {
            //          options.Endpoint = new Uri($"http://{otelServer}:9411/api/v2/spans");
            //      })
            //      .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
            //    );
            //}

            //services.AddLogging(logging =>
            //{
            //    logging.AddOpenTelemetry(options =>
            //    {
            //        options.AddConsoleExporter();
            //    });
            //});

            #endregion OPENTELEMETRY

            #region INFRASTRUCTURE


            #endregion INFRASTRUCTURE

            #region COMPATIBILITY
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            #endregion COMPATIBILITY


            #region INJECTIONS

            services.AddDbContext<CreditoAutomotrizContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ConnectionSqlServer")),
                    ServiceLifetime.Singleton
            );

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IArchivosService, ArchivosService>();
            services.AddTransient<IClientesService, ClientesService>();
            services.AddTransient<IVehiculosService, VehiculosService>();
            services.AddTransient<IPatiosAutosService, PatiosAutosService>();
            services.AddTransient<ICreditosService, CreditosService>();
            services.AddTransient<IClientesPatiosAutosService, ClientesPatiosAutosService>();

            services.AddTransient(typeof(IArchivosRepository<>), typeof(ArchivosRepository<>));
            services.AddTransient<IUnitWork, UnitWork>();
            services.AddHostedService<AppHost>();
            #endregion INJECTIONS


            //#region HANDLING API VERSIONS
            //services.AddApiVersioning(options => options.UseApiBehavior = true);
            //services.AddApiVersioning(options => options.AssumeDefaultVersionWhenUnspecified = true);
            //#endregion HANDLING API VERSIONS

            #region POLICY FOR CROSS DOMAIN
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                   .AllowAnyMethod()
                                                                   .AllowAnyHeader()));
            #endregion POLICY FOR CROSS DOMAIN

            #region Swagger
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "CreditoAutomotriz",
            //        Description = "Una descripcion del microservicio",
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Banco Pichincha",
            //            Email = "devops@pichincha.com",
            //            Url = new Uri("https://www.pichincha.com"),
            //        }
            //    });

            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
            //});

            #endregion Swagger
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region SwaggerUI
            app.UseStaticFiles();
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CreditoAutomotriz API");
            //    c.RoutePrefix = "swagger";
            //    c.InjectStylesheet("/swagger/custom.css");
            //});
            #endregion Swagger
            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
