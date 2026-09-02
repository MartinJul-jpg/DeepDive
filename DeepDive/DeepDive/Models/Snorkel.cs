using System.Drawing;

namespace DeepDive.Models
{
    public class Snorkel : Product
    {
        public string Model { get; set; }

        public override string GetUniqueInfo()
        {
            return $"{this.GetProductInfo()} Model: {Model}{"\n"}";
        }
        public override string GetUniqueInfoPage()
        {
            return $"Model: {Model}{"\n"}";
        }
    }
}
