using System;

namespace MarsRoversTask
{
    public class MarsRover
    {
        private PositionInfo _position;
        private Enums.rover Id { get; set; }
        public string Instructions { get; set; }
        
        public MarsRover(Enums.rover id, int x, int y, Enums.headingDirection z)
        {
            Id = id;
            _position = new PositionInfo(x, y, z);
        }

        public void SetNewPosition(int x, int y, Enums.headingDirection z)
        {
            _position = new PositionInfo(x, y, z);
            PrintCurrentPosition();
        }
        
        public void ApplyInstructions(int xLimit, int yLimit)
        {
            Console.WriteLine($"Rover #{Id} executing instructions: {Instructions}.");
            var steps = Instructions.ToUpper().ToCharArray();
            foreach (var step in steps)
            {
                switch (step)
                {
                    case 'L':
                        _position.SetHeading(Enums.rotateDirection.Left);
                        break;
                    case 'R':
                        _position.SetHeading(Enums.rotateDirection.Right);
                        break;
                    case 'M':
                        Move(xLimit,yLimit);
                        break;
                }
            }

            Instructions = string.Empty;
        }
        private void Move(int xLimit, int yLimit)
        {
            switch (_position.Z)
            {
                case Enums.headingDirection.North:
                    if (_position.Y + 1 <= yLimit) _position.Y += 1;
                    break;
                case Enums.headingDirection.East:
                    if (_position.X +1 <= xLimit) _position.X += 1;;
                    break;
                case Enums.headingDirection.South:
                    if (_position.Y - 1 >= 0) _position.Y -= 1;
                    break;
                case Enums.headingDirection.West:
                    if (_position.X -1 >= 0) _position.X -= 1;;
                    break;
            }
        }

        public void PrintCurrentPosition()
        {
            Console.WriteLine($"Rover #{Id} current position is:  [X: {_position.X}, Y: {_position.Y}, Heading:{_position.Z}]:");
        } 
    }
}