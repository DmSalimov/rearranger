using Level;

namespace Cells
{
    public class Platform: Cell
    {
        public override LevelItemType GetCellType() => LevelItemType.Platform;
    }
}