using Cells;
using Cells.Intrfeces;
using Helpers;

namespace Managers
{
    public class MoveManager : IMover
    {
        private GameMap GameMap { set; get; }

        public MoveManager(GameMap gameMap)
        {
            GameMap = gameMap;
        }

        private void RegisterMove(Coordinate from, Coordinate to) => GameMap.RegisterMove(from, to);

        public bool TryMove(IMovable who, Direction direction, bool skipAction = true)
        {
            var from = who.GetCoordinate();
            var to = new Coordinate(from.X + direction.X, from.Z + direction.Z);
            var toInArea = GameMap.Floor.TryGetValue(to, out Cell floorInDirection);
            var isWhoInDirection = GameMap.Map.TryGetValue(to, out Cell whoInDirection);
            IMovable whoInDirectionMovable = whoInDirection as IMovable;

            if (toInArea)
            {
                if (isWhoInDirection)
                {
                    // Debug.Log( whoInDirection.GetCellType());
                    if (whoInDirectionMovable != null)
                    {
                        var canMoveAccess = who.TryMove(whoInDirectionMovable, floorInDirection);

                        if (canMoveAccess)
                        {
                            var canPush = TryMove(whoInDirectionMovable, direction);
                            if (!canPush)
                            {
                                // Debug.Log("TryMove::false canPush");
                                return false;
                            }

                            RegisterMove(from, to);
                            if (!skipAction)
                            {
                                CheckActionable();
                            }
                            
                            return true;
                        }

                        // Debug.Log(String.Format("TryMove::canMoveAccess:: {0}", canMoveAccess));
                    }
                }
                else
                {
                    RegisterMove(from, to);
                    if (!skipAction)
                    {
                        CheckActionable();
                    }
                    return true;
                }
            }

            // Debug.Log(String.Format("TryMove::false exit toInArea:: {0}", toInArea));

            return false;
        }

        public void Move(IConnectable trailer, Coordinate from,  Coordinate to)
        {
            RegisterMove(from, to);

            if (!trailer.IsLast())
            {
                Move(trailer.GetTrailer(), trailer.GetTrailer().GetCoordinate() , from);
            }
        }

        private void CheckActionable()
        {
            foreach (var actionable in GameMap.Actionables)
            {
                actionable.Action();
            }
        }
    }
}