
using WebAPI_IOC_Samples.Services;

namespace WebAPI_IOC_Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Initialisierung Block für Dienste, die wir im IOC Container hinterlegen
            // Add services to the container.

            //AddController wird für die WebAPI verwendet 
            builder.Services.AddControllers();

            //builder.Services.AddControllersWithViews(); //MVC - UI Framework
            //builder.Services.AddRazorPages(); //Razor Page - UI-Framework -> ist gleich auf mit MVC zu sehen 
            //builder.Services.AddMvc(); //MVC und Razor Pages together

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //TimeService wurde hinterlegt 

            //Normale einfache Implementierung 
            //builder.Services.AddSingleton<ITimeService, TimeService>();

            //Einbinden eines TimeService via Extention-Methode -> TimeServiceExtentions.AddSingleton
            builder.Services.AddTimeService();


            var app = builder.Build(); //Hier passiert der Befehl intern-> BuildServiceProvider() 
            #endregion


            #region Erster Zugriff auf IOC Container

            ITimeService? timeService1 = app.Services.GetService<ITimeService>();
            ITimeService timeService2 = app.Services.GetRequiredService<ITimeService>();

            //via CreateScope -> .NET Core 2.1 gängige Methode
            using (IServiceScope scope =  app.Services.CreateScope())
            {
                ITimeService? timeService3 = scope.ServiceProvider.GetService<ITimeService>();
                ITimeService timeService4 = scope.ServiceProvider.GetRequiredService<ITimeService>();
            }

            #endregion

            #region Dienste möchten auch Konfiguriert werden 
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); // Request-Anfrage findet den richtigen Controller + Methode zum Auufruf -> Wird für WebAPI - Projekte verwendet
            #endregion

            //Starten der App 
            app.Run();
        }
    }


    public static class TimeServiceExtentions
    {
        public static void AddTimeService(this IServiceCollection collection)
            => collection.AddSingleton<ITimeService, TimeService>();    

    }
}