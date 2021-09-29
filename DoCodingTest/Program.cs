using System;
using System.Globalization;
using CandidateTest.TrainsRoutes;
using MarsRoversTask;

namespace DoCodingTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            var showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose a task option:");
            Console.WriteLine("1) Mars Rovers");
            Console.WriteLine("2) Trains");
            Console.WriteLine("3) Exit");
            Console.Write("\r\nSelect an option: ");
 
            switch (Console.ReadLine())
            {
                case "1":
                    MarsRoversTask();
                    return true;
                case "2":
                    TrainsTask();
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }
        private static void TrainsTask()
        {
            var Trains = new RoutesCalculator();
            Console.WriteLine(Trains.CalculateDistance("A-B-C"));
            Console.WriteLine(Trains.CalculateDistance("A-D"));
            Console.WriteLine(Trains.CalculateDistance("A-D-C"));
            Console.WriteLine(Trains.CalculateDistance("A-E-B-C-D"));
            Console.WriteLine(Trains.CalculateDistance("A-E-D"));

            Console.WriteLine(Trains.GetNumberOfTripsBetweenStations("C", "C", 3, false));
            Console.WriteLine(Trains.GetNumberOfTripsBetweenStations("A", "C", 4, true));
            Console.WriteLine(Trains.GetNumberOfTripsBetweenStations("A", "C", 2, true));
            Console.WriteLine(Trains.GetNumberOfTripsBetweenStations("B", "B", 3, true));
            Console.WriteLine(Trains.GetAllPathsLimitByTotalDistance("C", "C", 30));
            Console.ReadLine();
        }

        private static void MarsRoversTask()
        {
            if (GetInputGridLimits(out var xGridLimit, out var yGridLimit)) return;
            var marsRoversApp = new MarsRoversController(xGridLimit, yGridLimit);


            Console.WriteLine("Initializing rovers positions");
            //Rover 01 - Settings
            marsRoversApp.SetInitialPositionForRover(Enums.rover.One, 1, 2, Enums.headingDirection.North);
            //Rover 02 - Settings
            marsRoversApp.SetInitialPositionForRover(Enums.rover.Two, 3, 3, Enums.headingDirection.East);
            Console.WriteLine("\n");


            marsRoversApp.SetInstructionsForRover(Enums.rover.One, "LMLMLMLMM");
            marsRoversApp.SetInstructionsForRover(Enums.rover.Two, "MMRMMRMRRM");
            marsRoversApp.SendCommandsToRovers();
            Console.WriteLine("\n");

            marsRoversApp.PrintRoversCurrentPositions();
            Console.ReadLine();
        }

        private static bool GetInputGridLimits(out int xGridLimit, out int yGridLimit)
        {
            Console.WriteLine(
                "Insert grid limits [xLimit] [yLimit]. Values should be greater than zero. Separate values with spaces");
            var gridLimits = Console.ReadLine().Trim();
            var limits = gridLimits.Split(" ");
            if (limits.Length < 2)
            {
                Console.WriteLine("Some grid limits values are missing.");
                Console.ReadLine();
                xGridLimit = 0;
                yGridLimit = 0;
                return true;
            }

            xGridLimit = 0;
            yGridLimit = 0;

            if (!int.TryParse(limits[0], out xGridLimit) || !int.TryParse(limits[1], out yGridLimit))
            {
                Console.WriteLine("Grid limits are not numeric.");
                Console.ReadLine();
                return true;
            }

            return false;
        }
    }
}