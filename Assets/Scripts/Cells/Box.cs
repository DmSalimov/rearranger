using Level;
using UnityEngine;

namespace Cells
{
    public class Box: Cell, IMove
    {
        public override LevelItemType GetType() => LevelItemType.Box;
        
        public bool TryMove(IMove whom)
        {
            switch (whom)
            {
                case Player player:
                    return true;
                case Box box:
                    return true;
                case null:
                    return true;
                default:
                    return false;
            }
        }

    }
}
