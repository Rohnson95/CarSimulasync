﻿namespace CarSim2
{
    public class RaceLogic
    {
        public static async Task RaceMain() //Main RaceLogic
        {
            Console.WriteLine("Car Sim! Press enter to start");
            Console.ReadKey();
            Console.WriteLine("Race has started!");

            //Car objects
            Car firstCar = new Car()
            {
                Id = 1,
                Name = "Lightning",
                DistanceLeft = 10000,
                DistanceDriven = 0,
                Speed = 120,
                CarTime = 0,

                CarStatus = "None"
            };
            Car secondCar = new Car()
            {
                Id = 2,
                Name = "Rockie",
                DistanceLeft = 10000,
                DistanceDriven = 0,
                Speed = 120,
                CarTime = 0,
                CarStatus = "None"

            };
            Car thirdCar = new Car()
            {
                Id = 3,
                Name = "Guido",
                DistanceLeft = 10000,
                DistanceDriven = 0,
                Speed = 120,
                CarTime = 0,
                CarStatus = "None"
            };


            bool winner = false;

            //calls the startRace method for each object
            var firstCarTask = StartRace(firstCar);
            var secondCarTask = StartRace(secondCar);
            var thirdCarTask = StartRace(thirdCar);
            var statusCarTask = CarStatus(new List<Car> { firstCar, secondCar, thirdCar });
            var carTasks = new List<Task> { firstCarTask, secondCarTask, thirdCarTask, statusCarTask };

            //While there are more than 0 cars left, keep going
            while (carTasks.Count > 0)
            {

                Task finishedTask = await Task.WhenAny(carTasks);
                if (finishedTask == firstCarTask)
                {
                    Car carResult = firstCarTask.Result;
                    if (winner == false)
                    {
                        winner = true;
                        Console.WriteLine($"{firstCar.Name} Won! Time: {String.Format("{0:F2}", firstCar.CarTime)}");

                    }
                    else Console.WriteLine($"{firstCar.Name} has Passed the Finish line with a time of {String.Format("{0:F2}", firstCar.CarTime)}s");


                }
                else if (finishedTask == secondCarTask)
                {
                    Car carResult = secondCarTask.Result;
                    if (winner == false)
                    {
                        winner = true;
                        Console.WriteLine($"{secondCar.Name} Won!  Time: {String.Format("{0:F2}", secondCar.CarTime)}s");

                    }
                    else Console.WriteLine($"{secondCar.Name} has Passed the Finish line with a time of {String.Format("{0:F2}", secondCar.CarTime)}s");

                }
                else if (finishedTask == thirdCarTask)
                {
                    if (winner == false)
                    {
                        winner = true;
                        Console.WriteLine($"{thirdCar.Name} Won!  Time: {String.Format("{0:F2}", thirdCar.CarTime)}s");

                    }
                    else Console.WriteLine($"{thirdCar.Name} has Passed the Finish line with a time of {String.Format("{0:F2}", thirdCar.CarTime)}s");

                }
                else if (finishedTask == statusCarTask)
                {
                    Console.WriteLine("All cars are through!");
                }

                await finishedTask;
                carTasks.Remove(finishedTask);

            }



        }
        public async static Task Wait(int delay = 1) //creates a Wait method that has a task delay of one second 
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
        }

        //Main Racelogic
        public async static Task<Car> StartRace(Car car)
        {
            int time = 30;
            Random random = new Random();
            while (true)
            {
                if (car.CarTime % 30 == 0)
                {
                    int eventProbability = random.Next(1, 51); //creates an instance of randomness where any of the following items can happened based on odds
                    if (eventProbability == 1)
                    {
                        Console.WriteLine($"{car.Name} ran out of Gas! stopping to refuel...");
                        car.CarStatus = "Ran out of gas"; //Sets the carstatus from none to "ran out of gas"
                        //await Wait(30);
                        await Wait(30);
                        //car.CarTime = car.CarTime + 30;
                        car.Event += 30;
                    }
                    else if (eventProbability <= 3)
                    {
                        Console.WriteLine($"{car.Name} got a flat tire!");
                        car.CarStatus = "Flat tire";
                        await Wait(20);
                        //car.CarTime = car.CarTime + 20;
                        car.Event += 20;
                    }
                    else if (eventProbability >= 3 && eventProbability <= 7)
                    {
                        Console.WriteLine($"{car.Name} got a bird on the windshield! Cleaning windshield...");
                        car.CarStatus = "Bird On The Windshield";
                        await Wait(10);
                        //car.CarTime = car.CarTime + 10;
                        car.Event += 10;


                    }
                    else if (eventProbability >= 8 && eventProbability <= 17)
                    {
                        Console.WriteLine($"{car.Name} got a mechanical issue! Slowed by 1km/h");
                        car.Speed -= 1;
                    }
                }
                double speed = car.Speed / 3.6;

                await Wait(time);
                car.CarTime += time;

                car.DistanceLeft -= (speed * time);
                car.TimeToFinish = (car.DistanceLeft / speed) + car.Event;

                //Console.WriteLine($"distance Left is {car.DistanceLeft} Cartime {car.CarTime}");

                if (car.TimeToFinish <= 30) //Adds time onto the CarTime variable so that the accuracy of the car that wins is precise.
                {
                    car.CarTime += car.TimeToFinish;
                    car.DistanceLeft = 0;
                    car.TimeToFinish = 0;
                    return car;
                }

            }
        }

        //Method to call for a statusupdate during the race
        public async static Task CarStatus(List<Car> cars)
        {

            while (true)
            {

                DateTime start = DateTime.Now;
                bool gotKey = false;
                while ((DateTime.Now - start).TotalSeconds < 30)
                {
                    if (Console.KeyAvailable)
                    {
                        gotKey = true;
                        break;
                    }
                }
                if (gotKey)
                {
                    Console.ReadKey();
                    Console.Clear();
                    cars.ForEach(car =>
                    {
                        PrintCar(car);
                    });
                    gotKey = false;
                }
                await Task.Delay(10);
                var totalRemaining = cars.Select(car => car.TimeToFinish).Sum();
                if (totalRemaining == 0)
                {
                    return;
                }
            }
        }
        //Prints the information for each car
        public static void PrintCar(Car car)
        {

            Console.WriteLine($"{car.Name} has {String.Format("{0:F2}", car.DistanceLeft)} left and is going at {car.Speed} km/h | Problems: {car.CarStatus}");

        }
    }
}

