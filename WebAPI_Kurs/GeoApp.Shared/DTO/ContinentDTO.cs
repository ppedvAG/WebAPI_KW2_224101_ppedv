namespace GeoApp.Shared.DTO
{
    public class ContinentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public IList<CountryDTO> CountriesDTOs { get; set; }
    }
}
