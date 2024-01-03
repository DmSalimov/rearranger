namespace Cells
{
    public interface IMove
    {
        bool TryMove(IMove whom);
        Coordinate GetCoordinate();
    }
}
