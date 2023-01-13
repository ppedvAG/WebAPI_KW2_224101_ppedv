using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Grundlagen.Models
{
    public class Continent
    {
        public int Id { get; set; }
        public string Name { get; set; }


        
        public virtual ICollection<Country> Countries { get; set; }

    }
}
