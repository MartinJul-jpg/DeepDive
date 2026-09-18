using System.Drawing;

namespace DeepDive.Models
{
    public class Tank : Product
    {
        public string Volume { get; set; } = string.Empty;

        public override string GetUniqueInfo()
        {
            return $"{this.GetProductInfo()} Volume: {Volume}{"\n"}";
        }
        public override string GetUniqueInfoPage()
        {
            return $"{Volume}";
        }
    }
}
