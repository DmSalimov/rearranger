namespace Cells.Intrfeces
{
    public interface IMovable
    {
        bool TryMove(IMovable whom, Cell floorInDirection);
        Coordinate GetCoordinate();
    }
}
