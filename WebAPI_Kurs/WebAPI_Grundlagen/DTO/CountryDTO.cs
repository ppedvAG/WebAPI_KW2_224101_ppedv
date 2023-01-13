namespace WebAPI_Grundlagen.DTO
{
    public class CountryDTO
    {
        public int Id { get;set; }

        public string Name { get;set; }

        public int Population { get; set; }

        public string Capitol { get; set; }

        public int ContinentId { get; set; }
    }
}
