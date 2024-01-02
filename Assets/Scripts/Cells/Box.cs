using System;

namespace Cells
{
    public class Box: BaseCell, ICellObject
    {
        public bool TryMove(ICellObject whom)
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
                    throw new ArgumentException("Необходимо обработать новые объекты");
            }
        }

    }
}
