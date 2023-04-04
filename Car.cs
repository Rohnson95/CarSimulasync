namespace CarSim2
{
    public class Car
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public decimal DistanceDriven { get; set; }
        public decimal DistanceLeft { get; set; }
        public int Speed { get; set; }
        public decimal CarTime { get; set; }

        public decimal TimeToFinish { get; set; }
    }
}
