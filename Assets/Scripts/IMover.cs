public interface IMover
{
    public bool TryMove(ICellObject who, Direction direction);
}