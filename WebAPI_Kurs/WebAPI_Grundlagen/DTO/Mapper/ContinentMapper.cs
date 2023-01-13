using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI_Grundlagen.Models;

namespace WebAPI_Grundlagen.DTO.Mapper
{
    public static class ContinentMapper
    {
        public static ContinentDTO ToDTO(this Continent continent)
        {
            return new ContinentDTO { Id = continent.Id, Name = continent.Name };
        }

        public static IList<ContinentDTO> ToDTOs(this IList<Continent> continents)
        {
            IList<ContinentDTO> result = new List<ContinentDTO>();  

            foreach (Continent continent in continents)
            {
                result.Add(ToDTO(continent));
            }

            return result;
        }

        public static Continent ToEntity(this ContinentDTO continentDTO)
        {
            return new Continent { Id = continentDTO.Id, Name = continentDTO.Name };
        }

        public static IList<Continent> ToEntities(this IList<ContinentDTO> continentDTOs)
        {
            IList<Continent> continents = new List<Continent>();

            foreach(ContinentDTO currentDTO in continentDTOs)
                continents.Add(ToEntity(currentDTO));

            return continents;
        }


    }
}
