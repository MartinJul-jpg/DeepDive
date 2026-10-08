namespace Shared.Models
{
    public class Snorkel : Product
    {
        public string? Model { get; set; } = string.Empty;

        public override string GetUniqueInfo()
        {
            return $"{this.GetProductInfo()} Model: {Model}{"\n"}";
        }
        public override string GetUniqueInfoPage()
        {
            return $"{Model}";
        }

        public override string GetUniqueSearchBlob()
        {
            return $"{Model}";
        }
    }
}
