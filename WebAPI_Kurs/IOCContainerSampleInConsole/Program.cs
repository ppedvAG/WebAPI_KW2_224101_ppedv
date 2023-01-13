using Microsoft.Extensions.DependencyInjection;

namespace IOCContainerSampleInConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region Beispiel 1 
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
            #endregion


            #region Beispiel 2
            IServiceCollection serviceCollection2 = new ServiceCollection();

            serviceCollection2.AddSingleton<ITimeService, TimeService>();
            //serviceCollection2.AddSingleton<ITimeService, TimeService2>();

            serviceCollection2.AddScoped<ITimeService, TimeService2>();

            IServiceProvider serviceProvider2 = serviceCollection2.BuildServiceProvider();

            ITimeService welcherTimeServiceWirdGeladen = serviceProvider2.GetService<ITimeService>();

            Console.WriteLine(welcherTimeServiceWirdGeladen.ShowTime());


            //Lösungen wie man ein Interface mit mehrren Implementierungen verwenden kann:
            // -> https://medium.com/geekculture/net6-dependency-injection-one-interface-multiple-implementations-983d490e5014
            // -> https://learn.microsoft.com/de-de/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-7.0
            #endregion
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

    public class TimeService2 : ITimeService
    {
        private string _time;


        //ctor + tab + tab -> Konstruktor
        public TimeService2()
        {
            _time = DateTime.Now.ToShortTimeString();
        }


        public string ShowTime()
        {
            return "TimeService2 -> " + _time;
        }
    }
}