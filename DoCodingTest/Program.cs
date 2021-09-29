using System;
using System.Globalization;
using System.Windows.Markup;
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
            Console.Clear();
            Console.WriteLine("Stations available: 'A' 'B' 'C' 'D' 'E'");
            Console.WriteLine("Choose action to execute:"); 
            Console.WriteLine("1) Get distance for route (input samples: 'A-D-C', 'A-D' ).");
            Console.WriteLine("2) Get number of feasible trips between 2 stations (with max number of stops as limit)");
            Console.WriteLine("3) Get number of feasible trips between 2 stations (with exact number of stops)");
            Console.WriteLine("4) Get number of feasible paths between 2 stations (with total distance as limit)");
            Console.WriteLine("5) Get total distance of the shortest route between 2 stations");
            Console.WriteLine("6) Exit");
            Console.Write("\r\nSelect an option: ");
            
            var Trains = new RoutesCalculator();
            
            switch (Console.ReadLine())
            {
                case "1":
                    CalculateDistanceOnPath(Trains);
                    return;
                case "2":
                    CalculateNumberOfTripsQuery(Trains, false);
                    return;
                case "3":
                    CalculateNumberOfTripsQuery(Trains, true);
                    return;
                case "4":
                    CalculateNumberOfTripsQueryWithDistanceLimit(Trains);
                    return;
                case "5":
                    CalculateDistanceOnShortestPathQuery(Trains);
                    return;
                case "6":
                    return;
                default:
                    return;
            }
            
            
            
            
            // Console.WriteLine(Trains.CalculateDistance("A-B-C"));
            // Console.WriteLine(Trains.CalculateDistance("A-D"));
            // Console.WriteLine(Trains.CalculateDistance("A-D-C"));
            // Console.WriteLine(Trains.CalculateDistance("A-E-B-C-D"));
            // Console.WriteLine(Trains.CalculateDistance("A-E-D"));
            //
            // Console.WriteLine(Trains.GetNumberOfTripsBetweenStations("C", "C", 3, false));
            // Console.WriteLine(Trains.GetNumberOfTripsBetweenStations("A", "C", 4, true));
            // Console.WriteLine(Trains.GetNumberOfTripsBetweenStations("A", "C", 2, true));
            // Console.WriteLine(Trains.GetNumberOfTripsBetweenStations("B", "B", 3, true));
            // Console.WriteLine(Trains.GetAllPathsLimitByTotalDistance("C", "C", 30));
            // Console.ReadLine();
        }

        private static void CalculateDistanceOnPath(RoutesCalculator Trains)
        {
            var routePath = Console.ReadLine();
            Console.WriteLine($"Distance for path {routePath} = {Trains.CalculateDistance(routePath)}");
            Console.ReadLine();
        }

        private static void CalculateNumberOfTripsQuery(RoutesCalculator Trains, bool matchMaxStops)
        {
            Console.Write("From station: ");
            var stationA = Console.ReadLine();
            Console.Write("To station: ");
            var stationB = Console.ReadLine();
            Console.Write("Max Stops: ");
            var maxStops = Console.ReadLine();

            var numberOfTrips =
                Trains.GetNumberOfTripsBetweenStations(stationA, stationB, int.Parse(maxStops), matchMaxStops);

            Console.WriteLine($"Number of trips: {numberOfTrips}");
            Console.ReadLine();
        }
        
        private static void CalculateDistanceOnShortestPathQuery(RoutesCalculator Trains)
        {
            Console.Write("From station: ");
            var stationA = Console.ReadLine();
            Console.Write("To station: ");
            var stationB = Console.ReadLine();
            
            // Console.WriteLine(Trains.GetNumberOfTripsBetweenStations("A", "C", 2, true));
            var numberOfTrips =
                Trains.GetNumberOfTripsBetweenStations(stationA, stationB, 2, true);

            Console.WriteLine($"Number of trips: {numberOfTrips}");
            Console.ReadLine();
        }
        
        private static void CalculateNumberOfTripsQueryWithDistanceLimit(RoutesCalculator Trains)
        {
            Console.Write("From station: ");
            var stationA = Console.ReadLine();
            Console.Write("To station: ");
            var stationB = Console.ReadLine();
            Console.Write("Distance limit: ");
            var distance = Console.ReadLine();
            
            var numberOfTrips =
                Trains.GetAllPathsLimitByTotalDistance(stationA, stationB, int.Parse(distance));

            Console.WriteLine($"Number of different routes/path: {numberOfTrips}");
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