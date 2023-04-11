namespace CarSim2
{
    public class Car
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public double DistanceDriven { get; set; }
        public double DistanceLeft { get; set; }
        public double Speed { get; set; }
        public double CarTime { get; set; }
        public double Event { get; set; }
        public double TimeToFinish { get; set; }


        public string CarStatus { get; set; }

        public Car()
        {
            DistanceLeft = 10000;
            Speed = 120;
            TimeToFinish = DistanceLeft / (Speed / 3.6) + Event;
        }
    }
}
