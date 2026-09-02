namespace DeepDive.Models
{
    public class BCD : Product
    {
        public string Model{ get; set; }
        public string Size { get; set; }

        public override string GetUniqueInfo()
        {
            return $"{this.GetProductInfo()} Model: {Model}{"\n"} Size: {Size}{"\n"}";
        }
        public override string GetUniqueInfoPage()
        {
            return $"Model: {Model}{"\n"} Size: {Size}{"\n"}";
        }
    }
}
