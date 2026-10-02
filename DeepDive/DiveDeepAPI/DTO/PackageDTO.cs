namespace DiveDeepAPI.DTO
{
    public class PackageDTO
    {
            public int PackageId { get; set; }
            public List<int> PackageIds { get; set; } = new List<int>();

           // public string GetSearchBlob()
           // {
           //     string result = "";
           //     foreach (Package_Product pp in Package_Products)
           //     {
           //         result += $"{pp.Product.GetSearchBlob()} ";
           //     }
           //
           //     return result;
           // }
    }
}
