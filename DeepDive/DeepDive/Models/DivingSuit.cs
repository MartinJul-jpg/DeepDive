namespace DeepDive.Models
{
    public class DivingSuit : Product
    {
        public string Model { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Thickness { get; set; }

        public override string GetUniqueInfo()
        {
            return $"{this.GetProductInfo()} Model: {Model}{"\n"} Size: {Size}{"\n"} Type: {Type}{"\n"} Gender: {Gender}{"\n"} Thickness: {Thickness}{"\n"}";
        }
    }
}
