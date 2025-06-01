using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services - Add The Services To DI Container [6 Props]

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            // Add other services like DbContext, Repositories, etc. here
            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add Services of Generic Repo
            webApplicationBuilder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion

            var app = webApplicationBuilder.Build();

            #region Update database and Log Exceptopns using [ILoggerFactory]

            using var scope = app.Services.CreateScope(); // Range Of Time [Container Of Services]
            var services = scope.ServiceProvider; // Services [Objects] It Self 
            var _dbContext = services.GetRequiredService<StoreContext>();
            // Ask CLR FOR Creating Objects From DbContex
            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // for Log Exceptions at Console
            var logger = loggerFactory.CreateLogger<Program>();
            try
            {
                await _dbContext.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during migration");
            }
            #endregion

            #region DataSeeding 

            try
            {
                await StoreContextSeed.SeedAsync(_dbContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error Occured While Seeding Data");
            }

            #endregion

            #region Configure - Configure The HTTP Request Pipeline

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            //app.UseRouting(); // Add routing middleware
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller}/{action=index}/{id?}");
            //});


            app.MapControllers(); 

            #endregion

            app.Run();
        }
    }
}
