namespace DiveDeepAPI.Models
{
    public class BCD : Product
    {
        public string? Model{ get; set; }
        public string? Size { get; set; }

        public override string GetUniqueInfo()
        {
            return $"Model: {Model}{"\n"} Size: {Size}{"\n"}";
        }
        public override string GetUniqueInfoPage()
        {
            return $"{Model}";
        }

        public override string GetUniqueSearchBlob()
        {
            return $"{Model} {Size}";
        }
    }
}
