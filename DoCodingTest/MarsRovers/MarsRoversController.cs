using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MarsRoversTask
{
    public class MarsRoversController
    {
        private readonly int _xLimit;
        private readonly int _yLimit;

        private readonly Dictionary<Enums.rover, MarsRover> rovers;

        public MarsRoversController(int xLimit, int yLimit)
        {
            _xLimit = xLimit;
            _yLimit = yLimit;
            rovers = new Dictionary<Enums.rover, MarsRover>()
            {
                {Enums.rover.One, new MarsRover(Enums.rover.One,0,0,Enums.headingDirection.East)},
                {Enums.rover.Two, new MarsRover(Enums.rover.Two,0,0,Enums.headingDirection.East)}
            };
        }
        
        public void SendCommandsToRovers()
        {
            Console.WriteLine("Sending commands to rovers ...");
            
            foreach (var rov in rovers)
            {
                rov.Value.ApplyInstructions(_xLimit, _yLimit);
            }
        }

        public void SetInitialPositionForRover(Enums.rover roverNumber, int x, int y, Enums.headingDirection heading)
        {
            rovers[roverNumber].SetNewPosition(x, y, heading);
        }
        
        public void SetInstructionsForRover(Enums.rover roverNumber, string instructions)
        {
            rovers[roverNumber].Instructions = instructions;
        }

        public void PrintRoversCurrentPositions()
        {
            foreach (var rov in rovers)
            {
                rov.Value.PrintCurrentPosition();
            }
        }
    }
}