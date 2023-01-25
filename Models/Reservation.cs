using System.ComponentModel.DataAnnotations;

namespace Car_Rental_System.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public double Total_Price { get; set; }
    }
}
