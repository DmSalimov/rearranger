using Cells.Intrfeces;
using Helpers;
using Level;
using UnityEngine;

namespace Cells
{
    public class Trailer : Cell, IMovable, IConnectable, IActionable
    {
        [SerializeField] private Transform model;

        private IConnectable _head = null;
        private IConnectable _trailer = null;

        private GameMap _gameMap;

        public void Init(GameMap gameMap)
        {
            _gameMap = gameMap;
        }

        public bool TryMove(IMovable whom, Cell floorInDirection)
        {
            switch (whom)
            {
                case Player player:
                case Trailer trailer:
                    return true;
            }

            return false;
        }

        public override LevelItemType GetCellType() => LevelItemType.Trailer;

        public override void SetCoordinate(Coordinate crd, bool smoothly = false)
        {
            if (smoothly)
            {
                Vector2 direction = new Vector2(coordinate.X - crd.X, coordinate.Z - crd.Z);
                model.eulerAngles = new Vector3(0, DirectionHelper.GetRotationByDirection(direction), 0);
            }

            base.SetCoordinate(crd, smoothly);
        }


        public bool IsConnected() => _head != null;

        public bool IsLast() => _trailer == null;
        public IConnectable GetTrailer() => _trailer;

        public void Connect(IConnectable who)
        {
            _trailer = who;
            _trailer.Joined(this);
        }

        public void Joined(IConnectable head)
        {
            _head = head;
        }

        public void Action()
        {
            if (!IsConnected())
            {
                TryConnecting();
            }
        }

        private void TryConnecting()
        {
            var up = TryConnectingOnDirection(Direction.Up());
            if (!up)
            {
                var right = TryConnectingOnDirection(Direction.Right());
                if (!right)
                {
                    var down = TryConnectingOnDirection(Direction.Down());
                    if (!down)
                    {
                        TryConnectingOnDirection(Direction.Left());
                    }
                }
            }
        }

        private bool TryConnectingOnDirection(Direction direction)
        {
            var map = _gameMap.Map;
            var haveUp = map.TryGetValue(GetCoordinateWithDirection(direction), out Cell upCell);
            var upConnectable = upCell as IConnectable;
            if (upConnectable != null)
            {
                if (upConnectable.IsLast())
                {
                    upConnectable.Connect(this);
                    return true;
                }
            }

            return false;
        }

        private Coordinate GetCoordinateWithDirection(Direction direction) =>
            new Coordinate(coordinate.X + direction.X, coordinate.Z + direction.Z);
    }
}