namespace WebAPI_IOC_Samples.Services
{
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
