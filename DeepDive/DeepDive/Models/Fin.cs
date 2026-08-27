namespace DeepDive.Models
{
    public class Fin : Product
    {
        public int FinId { get; set; }

        public string Model { get; set; }

        public string Size { get; set; }

        public double Price { get; set; }
    }
}
