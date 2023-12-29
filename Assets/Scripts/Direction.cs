public class Direction
{
    public int X { get; private set; }
    public int Z { get; private set; }

    public Direction(int x, int z)
    {
        X = x > 0 ? 1 : x < 0 ? -1 : 0;
        Z = z > 0 ? 1 : z < 0 ? -1 : 0;
    }
}