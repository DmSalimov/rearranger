namespace Cells
{
    public class Coordinate
    {
        public int X { get; }
        public int Z { get; }

        public Coordinate(int x, int z)
        {
            X = x;
            Z = z;
        }

        public override string ToString()
        {
            return "(X: " + X + " Z: " + Z +")";
        }

        public override bool Equals(object obj)
        {
            var item = obj as Coordinate;

            if (item == null)
            {
                return false;
            }

            return item.X == X && item.Z == Z;
        }

        public override int GetHashCode()
        {
            return X * 1000 + Z;
        }
    }
}