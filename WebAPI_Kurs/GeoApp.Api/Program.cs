
using GeoApp.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace GeoApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<GeoDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("GeoDbConcectionString"));
                options.UseLazyLoadingProxies(); //Relationen k�nnen wir via Lazy Loading erlangen -> anderes Konzpt w�re Eager-Loading
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}