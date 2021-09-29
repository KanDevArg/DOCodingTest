using System;
using CandidateTest.TrainsRoutes;
using MarsRoversTask;

namespace DoCodingTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            var marsRoversApp = new MarsRoversController(5, 5);
            
            
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
        }
    }
}