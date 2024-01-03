using Cells;

public interface IMover
{
    public bool TryMove(IMove who, Direction direction);
}