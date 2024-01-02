namespace Cells
{
    public interface ICellObject
    {
        bool TryMove(ICellObject whom);
        void SetCoordinate(Coordinate coordinate, bool smoothly = false);
        Coordinate GetCoordinate();
    }
}
