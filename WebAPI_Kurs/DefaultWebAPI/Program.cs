
namespace DefaultWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //appsettings.json wird ausgelesen und befindet hier schon im Arbeitsspeicher
            //An .NET 6.0 -> WebApplicationBuilder + WebApplication (Seperation of Concerne)
            
            //WebApplicatoinBuilder wird für die Initialsierung deiner WebAPP in ASPNET Core verwendet
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //WebApplication als Instanz, bildet deine WebApp ab.
            WebApplication app = builder.Build();


            #region Konfiguration Part
            // Configure the HTTP request pipeline.

            //Woher kann ich wissen, ob bei IsDevelopment() um eine Entwickler-Instanz handelt
            if (app.Environment.IsDevelopment())
            {
                //Swagger wird nur den Entwickler zugänglich gemacht, die die Development Porjekt-Instanz verwenden 
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}