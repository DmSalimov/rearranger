namespace Cells
{
    public interface IMove
    {
        bool TryMove(IMove whom, Cell floorInDirection);
        Coordinate GetCoordinate();
    }
}
