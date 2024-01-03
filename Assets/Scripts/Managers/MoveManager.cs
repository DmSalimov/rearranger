using System;
using Cells;
using UnityEngine;
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

        public bool TryMove(IMove who, Direction direction)
        {
            var from = who.GetCoordinate();
            var to = new Coordinate(from.X + direction.X, from.Z + direction.Z);
            var toInArea = GameMap.Floor.TryGetValue(to, out Cell floorInDirection);
            GameMap.Map.TryGetValue(to, out Cell whoInDirection);
            IMove whoInDirectionMove = whoInDirection as IMove;

            if (toInArea)
            {
                if (whoInDirection != null)
                {
                    if (whoInDirectionMove != null)
                    {
                        var canMoveAccess = who.TryMove(whoInDirectionMove, floorInDirection);

                        if (canMoveAccess)
                        {
                            var canPush = TryMove(whoInDirectionMove, direction);
                            if (!canPush)
                            {
                                Debug.Log("TryMove::false canPush");
                                return false;
                            }

                            RegisterMove(from, to);
                            return true;
                        }

                        Debug.Log(String.Format("TryMove::canMoveAccess:: {0}", canMoveAccess));
                    }
                }
                else
                {
                    RegisterMove(from, to);
                    return true;
                }
            }

            Debug.Log(String.Format("TryMove::false exit toInArea:: {0}", toInArea));

            return false;
        }
    }
}