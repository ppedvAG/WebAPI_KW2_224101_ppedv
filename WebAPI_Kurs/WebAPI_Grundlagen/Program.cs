using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using WebAPI_Grundlagen.Data;
using WebApiContrib.Core.Formatter.Csv;

namespace WebAPI_Grundlagen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            //EF Core wurde eingebunden 
            builder.Services.AddDbContext<CarDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CarDbContext") ?? throw new InvalidOperationException("Connection string 'CarDbContext' not found.")));


            builder.Services.AddDbContext<GeoDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(builder.Configuration.GetConnectionString("GeoDbContext") ?? throw new InvalidOperationException("Connection string 'GeoDbContext' not found."));
            });
            // Add services to the container.

            //Bei AddControllers, können wir noch weitere Formatter dranhängen
            builder.Services.AddControllers()
                .AddXmlSerializerFormatters()
                .AddCsvSerializerFormatters()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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