
using GeoApp.Shared.Entities;

namespace GeoApp.Shared.DTO.Mapper
{
    public static class CountryMapper
    {
        public static CountryDTO ToDTO(this Country country)
        {
            CountryDTO dto = new CountryDTO() { Id = country.Id, Name = country.Name, Capitol = country.Capitol, ContinentId = country.ContinentId };

            if (dto.ContinentId.HasValue)
            {
                dto.ContinentRef = country.ContinentRef;
            }
            return dto;
        }

        public static IList<CountryDTO> ToDTOs(this IList<Country> countries)
        {
            IList<CountryDTO> countryDTOs = new List<CountryDTO>();

            foreach (Country country in countries)
            {
                countryDTOs.Add(ToDTO(country));
            }

            return countryDTOs;
        }


        public static Country ToEntity(this CountryDTO country)
        {
            return new Country() { Id = country.Id, Name = country.Name, Capitol = country.Capitol, ContinentId = country.ContinentId };
        }

        public static IList<Country> ToEntities (this IList<CountryDTO> countries)
        {
            IList<Country> entities = new List<Country>();

            foreach (CountryDTO country in countries)
            {
                entities.Add(ToEntity(country));
            }
            return entities;    
        }
    }
}
