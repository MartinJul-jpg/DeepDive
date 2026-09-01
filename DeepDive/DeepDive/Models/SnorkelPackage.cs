namespace DeepDive.Models
{
    public class SnorkelPackage : Package
    {
        public SnorkelPackage(Snorkel snorkel, Fin fin)
        {
            base.setList = new List<Product>()
            {
            snorkel, fin
            };
        }
    }
}
