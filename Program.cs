namespace CarSim2
{
    class Program
    {
        Car firstCar = new Car()
        {
            Id = 1,
            Name = "Lightning",
            DistanceLeft = 10000,
            DistanceDriven = 0,
            Speed = 120,
            CarTime = 0
        };
        Car secondCar = new Car()
        {
            Id = 2,
            Name = "Rockie",
            DistanceLeft = 10000,
            DistanceDriven = 0,
            Speed = 120,
            CarTime = 0
        };

        static async Task Main(string[] args)
        {
            Console.WriteLine("Car Sim! Press enter to start");
            Console.ReadKey();
            Console.WriteLine("Race has started!");

            Car firstCar = new Car()
            {
                Id = 1,
                Name = "Lightning",
                DistanceLeft = 10000,
                DistanceDriven = 0,
                Speed = 120,
                CarTime = 0
            };
            Car secondCar = new Car()
            {
                Id = 2,
                Name = "Rockie",
                DistanceLeft = 10000,
                DistanceDriven = 0,
                Speed = 110,
                CarTime = 0
            };

            var firstCarTask = StartRace(firstCar);
            var secondCarTask = StartRace(secondCar);
            var statusCarTask = CarStatus(new List<Car> { firstCar, secondCar });
            var carTasks = new List<Task> { firstCarTask, secondCarTask, statusCarTask };
            ConsoleKeyInfo key = Console.ReadKey();
            bool check = true;
            while (check)
            {
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    foreach (var car in new List<Car> { firstCar, secondCar })
                    {
                        Console.WriteLine($"{car.Name}: {decimal.Round(car.DistanceLeft)} metres remaining");
                        if (car.DistanceLeft <= 0)
                        {
                            check = false;

                        }
                    }
                }
                //await Task.Delay(TimeSpan.FromSeconds(0.1));
            }

            while (carTasks.Count > 0)
            {

                Task finishedTask = await Task.WhenAny(carTasks);
                if (finishedTask == firstCarTask)
                {
                    Console.WriteLine($"{firstCar.Name} has passed finishline! time:{decimal.Round(firstCar.CarTime)} s");
                    Car carResult = firstCarTask.Result;
                    PrintCar(firstCar);

                }
                else if (finishedTask == secondCarTask)
                {
                    Console.WriteLine($"{secondCar.Name} has passed finishline! time:{decimal.Round(secondCar.CarTime)}s");
                    Car carResult = secondCarTask.Result;
                    PrintCar(secondCar);
                }
                else if (finishedTask == statusCarTask)
                {
                    Console.WriteLine("All cars are through!");
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

                car.DistanceLeft -= (speed * time);
                car.TimeToFinish = car.DistanceLeft / speed;

                //Console.WriteLine($"distance Left is {car.DistanceLeft} Cartime {car.CarTime}");

                if (car.TimeToFinish <= 30)
                {
                    car.CarTime += car.TimeToFinish;
                    car.DistanceLeft = 0;
                    return car;
                }

            }
        }

        public async static Task CarStatus(List<Car> cars)
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(30));
                Console.Clear();
                /*cars.ForEach(car =>
                {
                    Console.WriteLine($"{car.DistanceLeft} remaining");
                    //Console.WriteLine($"{car.Name} Has reached the finishline with a time of {car.CarTime}");
                });*/
                if (cars.Count == 0)
                {
                    return;
                }
            }
        }
        public static void PrintCar(Car car)
        {
            Console.WriteLine($"{car.Name} Has passed the finishline! with a time of {car.CarTime / 60} minutes");
        }

    }
}