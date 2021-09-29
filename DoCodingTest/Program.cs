using System;
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
        }
    }
}