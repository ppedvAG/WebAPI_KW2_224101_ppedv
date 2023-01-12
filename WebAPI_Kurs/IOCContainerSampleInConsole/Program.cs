using Microsoft.Extensions.DependencyInjection;

namespace IOCContainerSampleInConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IServiceCollection

            //Initialisierungphase eines Programms
            //ServiceCollection wird verwendet um beim Programmstart, alle Dienste zur Verfügung zu stellen 
            IServiceCollection serviceCollection = new ServiceCollection();


            //TimeService als Singleton -> TimeService-Instance wird nur 1x instaanziiert und läuft solange die WebApi läuft 
            serviceCollection.AddSingleton<ITimeService, TimeService>();

            //Pro Request wird 1x Instanz aufgebaut
            //serviceCollection.AddScoped<ITimeService, TimeService>();

            //Pro Zugriff auf IOC wird Instanz aufgebaut
            //serviceCollection.AddTransient<ITimeService, TimeService>();

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();


            //Herauslesen von ITimeService 

            //GetService vs. GetRequiredService 

            //GetService kann den Wert NULL enthalten, wenn ein Service im IOC nicht vorhanden ist
            ITimeService? timeService = serviceProvider.GetService<ITimeService>();


            //GetReuiredService wirft eine Exception, wenn ein Service im IOC nicht vorhanden ist
            ITimeService timeService1 = serviceProvider.GetRequiredService<ITimeService>();

            Console.WriteLine(timeService.ShowTime());


        }
    }


    public interface ITimeService
    {
        public string ShowTime();
    }

    public class TimeService : ITimeService
    {
        private string _time;


        //ctor + tab + tab -> Konstruktor
        public TimeService()
        {
            _time = DateTime.Now.ToShortTimeString();
        }


        public string ShowTime()
        {
            return _time;
        }
    }
}