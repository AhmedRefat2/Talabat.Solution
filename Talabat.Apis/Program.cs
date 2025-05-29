using Microsoft.EntityFrameworkCore;
using Talabat.Repository.Data;

namespace Talabat.Apis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services - Add The Services To DI Container 

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            // Add other services like DbContext, Repositories, etc. here
            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion

            var app = webApplicationBuilder.Build();

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
