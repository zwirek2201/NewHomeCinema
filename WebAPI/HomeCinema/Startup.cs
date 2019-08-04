using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeCinema.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HomeCinema
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
            services.AddCors(o =>
            {
                o.AddPolicy("myPolicy", p =>
                {
                    p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
                });
            });

            services.AddTransient<ICategoriesManager, CategoriesManager>();
            services.AddTransient<IMoviesManager, MoviesManager>();
            services.AddTransient<IRoomsManager, RoomsManager>();
            services.AddTransient<ISeatsManager, SeatsManager>();
            services.AddTransient<IUsersManager, UsersManager>();
            services.AddTransient<IScreeningsManager, ScreeningsManager>();
            services.AddTransient<IRepertoirManager, RepertoirManager>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("myPolicy");

            app.UseMvc();
        }
    }
}
