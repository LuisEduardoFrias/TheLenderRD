using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLenderRD.Domain.Services;
using TheLenderRD.Persistence.DbContext;

namespace TheLenderRD.WebApi
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

            services.AddDbContext<TheLenderRD_DBContext>(options =>
                options.UseSqlServer("Server=DESKTOP-9BPNFM1\\SQLEXPRESS; Database=TheLenderRD; Trusted_Connection=True;" 
                /*SettingStrings.ConnectionString*/, localmigration => 
                localmigration.MigrationsAssembly("TheLenderRD.WebApi")));

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("TheLenderRD", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API TheLender", Version = "1.0.0", Description = "The API allows you to make transactions to obtain information, save records and more." });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/TheLender/swagger.json", "API TheLender");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
