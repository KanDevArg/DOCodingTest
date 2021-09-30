namespace MarsRoversTask
{
   public struct PositionInfo
    {
        public int X
        {
            get => _x;
            set { _x = value; }
        }
        
        public int Y
        {
            get => _y;
            set { _y = value; }
        }
        
        public Enums.headingDirection Z
        {
            get => _z;
            set { _z = value; }
        }

        private int _x;
        private int _y;
        private Enums.headingDirection _z;
        

        public PositionInfo(int x, int y, Enums.headingDirection z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        /// <summary>
        /// Calculate new rover heading considering current heading plus rotation movement applied on the rover
        /// </summary>
        /// <param name="rotateDirection"></param>
        public void SetHeading(Enums.rotateDirection rotateDirection)
        {
            _z = Z switch
            {
                Enums.headingDirection.North => rotateDirection == Enums.rotateDirection.Right ? Enums.headingDirection.East : Enums.headingDirection.West,
                Enums.headingDirection.East => rotateDirection == Enums.rotateDirection.Right ? Enums.headingDirection.South : Enums.headingDirection.North,
                Enums.headingDirection.South => rotateDirection == Enums.rotateDirection.Right ? Enums.headingDirection.West : Enums.headingDirection.East,
                Enums.headingDirection.West => rotateDirection == Enums.rotateDirection.Right ? Enums.headingDirection.North : Enums.headingDirection.South,
                _ => _z
            };
        } 
    }
}