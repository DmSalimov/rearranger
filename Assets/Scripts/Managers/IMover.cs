using Cells;
using Helpers;

namespace Managers
{
    public interface IMover
    {
        public bool TryMove(IMove who, Direction direction);
    } 
}
