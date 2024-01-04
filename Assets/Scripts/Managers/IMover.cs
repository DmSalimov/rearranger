using Cells;
using Cells.Intrfeces;
using Helpers;

namespace Managers
{
    public interface IMover
    {
        public bool TryMove(IMovable who, Direction direction, bool skipAction = true);
        public void Move(IConnectable trailer, Coordinate from, Coordinate to);
    } 
}
