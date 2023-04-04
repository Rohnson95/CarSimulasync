namespace CarSim2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Car firstCar = new Car()
            {
                Id = 1,
                Name = "Lightning",
                DistanceLeft = 10000,
                DistanceDriven = 0,
                Speed = 119,
                CarTime = 0
            };

            var firstCarTask = StartRace(firstCar);
            var carTasks = new List<Task> { firstCarTask };


            while (carTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(carTasks);
                if (finishedTask == firstCarTask)
                {
                    Console.WriteLine($"First Car has passed finishline! time:{firstCar.CarTime}");
                    Car carResult = firstCarTask.Result;

                }
                await finishedTask;
                carTasks.Remove(finishedTask);
                Console.ReadKey();
            }



        }
        public async static Task Wait(int delay = 1)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay / 20));
        }

        public async static Task<Car> StartRace(Car car)
        {
            int time = 30;
            while (true)
            {
                decimal speed = car.Speed / 3.6M;


                await Wait(time);
                car.CarTime += time;


                /*if (car.CarTime < 30)
                {

                    car.DistanceLeft -= Speed * time;
                    
                }*/
                car.DistanceLeft -= (speed * time);
                car.TimeToFinish = car.DistanceLeft / speed;
                Console.WriteLine($"distance Left is {car.DistanceLeft} Cartime {car.CarTime}");

                if (car.TimeToFinish <= 30)
                {
                    car.CarTime += car.TimeToFinish;
                    car.DistanceLeft = 0;
                    return car;
                }



                //Console.WriteLine($"finished in {car.CarTime}");
                //car.DistanceLeft -= speed * time;
                //car.DistanceDriven += speed * time;



                /*if (car.DistanceDriven >= 10000)
                {
                    return car;
                }*/

            }
        }
    }
}