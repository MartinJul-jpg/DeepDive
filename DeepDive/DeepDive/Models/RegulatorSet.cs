using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace DeepDive.Models
{
    public class RegulatorSet : Product
    {
        public string FirstStage { get; set; } = string.Empty;
        public string SecondStage { get; set; }= string.Empty;
        public string Octopus { get; set; } = string.Empty;

        public override string GetUniqueInfo()
        {
            return $"{this.GetProductInfo()} FirstStage: {FirstStage}{"\n"} SecondStage: {SecondStage}{"\n"} Octopus: {Octopus}{"\n"}";
        }
        public override string GetUniqueInfoPage()
        {
            return $"{FirstStage} {SecondStage} {Octopus}";
        }
    }
}
