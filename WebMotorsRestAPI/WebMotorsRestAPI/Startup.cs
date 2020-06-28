using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebMotorsRestAPI.Model.Context;
using WebMotorsRestAPI.Services;
using WebMotorsRestAPI.Services.Implementations;

namespace WebMotorsRestAPI
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
            var connection = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connection));

            services.AddMvc();

            services.AddApiVersioning();

            services.AddScoped<IAnuncioService, AnuncioServiceImpl>();
            services.AddScoped<IMarcaService, MarcaServiceImpl>();
            services.AddScoped<IModeloService, ModeloServiceImpl>();
            services.AddScoped<IVersaoService, VersaoServiceImpl>();
            services.AddScoped<IVeiculoService, VeiculoServiceImpl>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
