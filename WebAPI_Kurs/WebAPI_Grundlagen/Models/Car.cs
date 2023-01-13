namespace WebAPI_Grundlagen.Models
{
    //POCO bzw Entity - Object
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }   

        public int ConstructionYear { get; set; }

    }
}
