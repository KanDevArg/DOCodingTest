using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Markup;
using CandidateTest.TrainsRoutes;
using DoCodingTest.SalesTaxes;
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
            Console.WriteLine("3) Taxes");
            Console.WriteLine("4) Exit");
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
                    Taxes();
                    return false;
                case "4":
                    return false;
                default:
                    return true;
            }
        }
        
        private static void Taxes()
        {
            Console.Clear();
            var Taxes = new ReceiptBuilder();
            var basket = new List<Product>();
            
            var book = new Product("Book", false, (decimal)12.49, ProductType.noTaxed);
            
            basket = new List<Product>()
             {
                 book,
                 book, 
                 new Product("Music CD", false, (decimal)14.99, ProductType.basicTaxed),
                 new Product("Chocolate Bar", false, (decimal)0.85, ProductType.noTaxed),
             };
            
            Taxes.PrintReceipt(basket);
            
            
            basket.Clear();
            basket = new List<Product>()
            {
                new Product("Imported box of chocolates", true, (decimal)10.00, ProductType.noTaxed),
                new Product("Imported bottle of perfume", true, (decimal)47.50, ProductType.basicTaxed),
            };
            
            Taxes.PrintReceipt(basket);
            
            
            basket.Clear();
            var importedChocolateBox = new Product("Imported box of chocolates", true, (decimal)11.25, ProductType.noTaxed);
            basket = new List<Product>()
            {
                new Product("Imported bottle of perfume", true,  (decimal)27.99, ProductType.basicTaxed),
                new Product("Bottle of perfume", false, (decimal)18.99, ProductType.basicTaxed),
                new Product("Packet of headache pills", false,  (decimal)9.75, ProductType.noTaxed),
                importedChocolateBox,
                importedChocolateBox
            };
            
            Taxes.PrintReceipt(basket);
            Console.ReadLine();
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
        }

        private static void CalculateDistanceOnPath(RoutesCalculator Trains)
        {
            Console.Write("Enter the route/path: ");
            var routePath = Console.ReadLine().ToUpper();
            Console.WriteLine($"Distance for route/path {routePath} = {Trains.CalculateDistance(routePath)}");
            Console.ReadLine();
        }

        private static void CalculateNumberOfTripsQuery(RoutesCalculator Trains, bool matchMaxStops)
        {
            Console.Write("From station: ");
            var stationA = Console.ReadLine().ToUpper();
            Console.Write("To station: ");
            var stationB = Console.ReadLine().ToUpper();
            Console.Write("Max Stops: ");
            var maxStops = Console.ReadLine().ToUpper();

            var numberOfTrips =
                Trains.GetNumberOfTripsBetweenStations(stationA, stationB, int.Parse(maxStops), matchMaxStops);

            Console.WriteLine($"Number of trips: {numberOfTrips}");
            Console.ReadLine();
        }
        
        private static void CalculateDistanceOnShortestPathQuery(RoutesCalculator Trains)
        {
            Console.Write("From station: ");
            var stationA = Console.ReadLine().ToUpper();
            Console.Write("To station: ");
            var stationB = Console.ReadLine().ToUpper();

            var shortestPath =  Trains.GetShortestTripBetweenStations(stationA, stationB, 12, false);
            
            
            Console.WriteLine($"Distance of shortest route {shortestPath}: {Trains.CalculateDistance(shortestPath)}");
            Console.ReadLine();
        }
        
        private static void CalculateNumberOfTripsQueryWithDistanceLimit(RoutesCalculator Trains)
        {
            Console.Write("From station: ");
            var stationA = Console.ReadLine().ToUpper();
            Console.Write("To station: ");
            var stationB = Console.ReadLine().ToUpper();
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
            Console.WriteLine("Initializing Rover #1");
            Console.Write("Rover #1 [x] coordinate: ");
            var rover1X = Console.ReadLine();
            Console.Write("Rover #1 [y] coordinate: ");
            var rover1Y = Console.ReadLine();
            Console.Write("Rover #1 Heading [1:North, 2:East, 3:West, 4:South]: ");
            var rover1Heading = Console.ReadLine();
            
            marsRoversApp.SetInitialPositionForRover(Enums.rover.One, int.Parse(rover1X), int.Parse(rover1Y), (Enums.headingDirection)int.Parse(rover1Heading));
            
            //Rover 02 - Settings
            Console.WriteLine("Initializing Rover #2");
            Console.Write("Rover #1 [x] coordinate: ");
            var rover2X = Console.ReadLine();
            Console.Write("Rover #1 [y] coordinate: ");
            var rover2Y = Console.ReadLine();
            Console.Write("Rover #1 Heading [1:North, 2:East, 3:West, 4:South]: ");
            var rover2Heading = Console.ReadLine();
            marsRoversApp.SetInitialPositionForRover(Enums.rover.Two, int.Parse(rover2X), int.Parse(rover2Y), (Enums.headingDirection)int.Parse(rover2Heading));
            Console.WriteLine("\n");

            //Instructions
            Console.WriteLine("Instructions for Rover #1");
            Console.WriteLine("Enter the sequence of commands. Sample: 'RLLMLMM'");
            Console.WriteLine("'R': Rotate right, 'L': Rotate left, 'M': Move.");
            
            var rover1Instructions = Console.ReadLine();
            marsRoversApp.SetInstructionsForRover(Enums.rover.One, rover1Instructions.ToUpper());
            
            
            Console.WriteLine("Instructions for Rover #2");
            Console.WriteLine("Enter the sequence of commands. Sample: 'RLLMLMM'");
            Console.WriteLine("'R': Rotate right, 'L': Rotate left, 'M': Move.");
            var rover2Instructions = Console.ReadLine();
            marsRoversApp.SetInstructionsForRover(Enums.rover.Two, rover2Instructions.ToUpper());
            
            marsRoversApp.SendCommandsToRovers();
            Console.WriteLine("\n");

            marsRoversApp.PrintRoversCurrentPositions();
            Console.ReadLine();
        }

        private static bool GetInputGridLimits(out int xGridLimit, out int yGridLimit)
        {
            Console.WriteLine("Insert grid limits [xLimit] [yLimit]. Values should be greater than zero.");
            
            Console.Write("x Grid limit: ");
            var xGridLimitInput = Console.ReadLine();
            
            Console.Write("y Grid limit: ");
            var yGridLimitInput = Console.ReadLine();

            if (!int.TryParse(xGridLimitInput, out xGridLimit ) || !int.TryParse(yGridLimitInput, out yGridLimit))
            {
                Console.WriteLine("Grid limits are not numeric.");
                Console.ReadLine();
                xGridLimit = 0;
                yGridLimit = 0;
                return true;
            }

            return false;
        }
    }
}