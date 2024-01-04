using Cells.Intrfeces;
using Level;

namespace Cells
{
    public class Box: Cell, IMovable
    {
        public override LevelItemType GetCellType() => LevelItemType.Box;
        
        public bool TryMove(IMovable whom, Cell floorInDirection)
        {
            switch (whom)
            {
                case Player player:
                case Box box:
                case null:
                    return true;
                default:
                    return false;
            }
        }

    }
}
