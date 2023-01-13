using GeoApp.Shared.Entities;

namespace GeoApp.Shared.DTO
{
    public class CountryDTO
    {
        public int Id { get;set; }

        public string Name { get;set; }


        public string Capitol { get; set; }

        public int? ContinentId { get; set; }

        public Continent? ContinentRef { get; set; }
    }
}
