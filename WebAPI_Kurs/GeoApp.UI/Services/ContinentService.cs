using GeoApp.Shared.Entities;
using Newtonsoft.Json;
using System.Text;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GeoApp.UI.Services
{
    //https://localhost:7038/api
    public class ContinentService : IContinentService
    {
        private HttpClient _httpClient;


        public ContinentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        //https://localhost:7038/api/Continents
        public async Task<IList<Continent>> GetAllContinents()
        {
            //Antwort unserer Abfrage wird in HttpResponseMessage gespeichert
            HttpResponseMessage response = await _httpClient.GetAsync("Continents");

            //Auslesen des JSON aus der Response
            string jsonText = await response.Content.ReadAsStringAsync();


            //https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-7-0
            List<Continent> continents = JsonConvert.DeserializeObject<List<Continent>>(jsonText);

            return continents;
        }

        public async Task AddContinent(Continent continent)
        {
            continent.Countries = new List<Country>();

            string jsonString = JsonConvert.SerializeObject(continent);

            StringContent data = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("Continents", data);

            string jsonText = await response.Content.ReadAsStringAsync();


            //https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-7-0
            Continent continentWithId  = JsonConvert.DeserializeObject<Continent>(jsonText);
        }

        public async Task DeleteContinent(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Continents/{id}");
        }

        

        public async Task UpdateContinent(Continent modifiedContinent)
        {
            string jsonString = JsonConvert.SerializeObject(modifiedContinent);
            StringContent data = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"Continents/{modifiedContinent.Id}", data);
        }

        public async Task<Continent> GetById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"Continents/{id}");

            //Auslesen des JSON aus der Response
            string jsonText = await response.Content.ReadAsStringAsync();


            //https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-7-0
            Continent continent = JsonConvert.DeserializeObject<Continent>(jsonText);

            return continent;
        }
    }
}
