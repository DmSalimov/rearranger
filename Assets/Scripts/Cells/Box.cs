using Level;
using UnityEngine;

namespace Cells
{
    public class Box: Cell, IMove
    {
        public override LevelItemType GetCellType() => LevelItemType.Box;
        
        public bool TryMove(IMove whom, Cell floorInDirection)
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
