using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_System.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }
        public bool IsBooked { get; set; }
        public double DailyHirePrice { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
