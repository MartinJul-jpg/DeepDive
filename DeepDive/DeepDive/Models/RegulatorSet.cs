namespace DeepDive.Models
{
    public class RegulatorSet : Product
    {
        public int RegulatorSetId { get; set; }

        public string FirstStage { get; set; }

        public string SecondStage { get; set; }

        public string Octopus { get; set; }
    }
}
