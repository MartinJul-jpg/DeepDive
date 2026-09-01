namespace DeepDive.Models
{
    public class DivingPackage : Package
    {
        public DivingPackage(BCD bcd, DivingSuit divingSuit, RegulatorSet regulatorSet, Tank tank, Fin fin, Snorkel snorkel)
        {
            base.setList = new List<Product>() 
            {
            bcd, regulatorSet, tank, fin, snorkel
            };
        }
    }
}
