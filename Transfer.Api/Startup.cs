using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Transfer.Domain;
using Transfer.Domain.Interfaces;
using Transfer.Domain.Services;
using Transfer.Infra.Data;
using Transfer.Infra.Data.Repositories;


namespace Transfer.Api
{
    public class Startup
    {
   

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        public IConfiguration Configuration { get; }

      
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);


            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("TransferApi"));
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<ITransferRepository, TransferRepository>();


            services.AddAutoMapper(typeof(Startup));


            #region RabbitMq

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);
            services.AddSingleton<IMessaging, MessagingService>();


            #endregion


            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Transfer API",
                    Version = "v1",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization"
                });

               
            });


            #endregion
        
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API"));


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
