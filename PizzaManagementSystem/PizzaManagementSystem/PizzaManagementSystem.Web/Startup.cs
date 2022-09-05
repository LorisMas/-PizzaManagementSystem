using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzaManagementSystem.Core.EntityServices;
using PizzaManagementSystem.Core.HelperServices;
using PizzaManagementSystem.Core.IoC;
using PizzaManagementSystem.Web.Mappings.Profiles;

namespace PizzaManagementSystem.Web
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
            services.AddDistributedMemoryCache();

            services.AddControllers();

            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MenuItemProfile>();
                cfg.AddProfile<OrderProfile>();
                cfg.AddProfile<OrderDetailProfile>();
            }).CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors();
            services.AddOptions();
        }


        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            //Registro il catalogo con tutti i miei servizi nel container.
            string connectionString = this.Configuration["Configuration:ConnectionString"];
            containerBuilder.RegisterModule(new ApplicationContainerModule(connectionString));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ConfigurationService configService, MenuItemService menuItemService)
        {
            //Leggo l'appsettings.json e recupero i parametri di configurazione.
            configService.ConnectionString = this.Configuration["Configuration:ConnectionString"];
            configService.AppName = this.Configuration["Configuration:AppName"];
            configService.IsDevelopment = env.IsDevelopment();

            if (bool.Parse(this.Configuration["Configuration:AddDefaultData"]))
                menuItemService.CreateDefaultMenu();

            app.UseStaticFiles();

            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
