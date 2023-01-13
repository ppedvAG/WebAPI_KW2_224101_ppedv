namespace WebAPI_Grundlagen.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Population { get; set; }

        public string Capitol { get; set; }

        public int ContinentId { get; set; }    

        public virtual Continent ContinentRef { get; set; } 
    }
}
