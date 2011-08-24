namespace MVVM.Life.Common.Models
{
    public class Coordinate : ICoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinate)
                return Equals(obj as Coordinate);
            return false;
        }
        public bool Equals(Coordinate other)
        {
            if (other == null)
                return false;
            return ((X == other.X) && (Y == other.Y));
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + X.GetHashCode();
            hash = (hash * 7) + Y.GetHashCode();

            return hash;
        }
    }
}
