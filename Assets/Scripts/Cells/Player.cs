using Cells.Components;
using Cells.Intrfeces;
using Helpers;
using Level;
using Managers;
using UnityEngine;

namespace Cells
{
    [RequireComponent(typeof(Connector))]
    public class Player : Cell, IMovable, IActionable
    {
        public override LevelItemType GetCellType() => LevelItemType.Player;

        [SerializeField] private Transform model;
        public bool MoveInProcess { get; private set; }
        private IMover _mover;
        private Connector connector;
        
        public void Init(IMover mover, GameMap gameMap)
        {
            _mover = mover;
            connector = GetComponent<Connector>();
            connector.Init(gameMap);
        }

        public bool TryMove(IMovable whom, Cell floorInDirection)
        {
            switch (whom)
            {
                case Player player:
                case Box box:
                case null:
                    return true;
                case Trailer trailer:
                    return false;
            }

            return false;
        }

        public override void SetCoordinate(Coordinate crd, bool smoothly = false)
        {
            var oldCoordinate = new Coordinate(coordinate.X, coordinate.Z);
            Vector2 direction = new Vector2(coordinate.X - crd.X, coordinate.Z - crd.Z);
            coordinate = crd;
            if (connector != null && !connector.IsLast())
            {
                _mover.MoveTrailer(connector.GetTrailer(), oldCoordinate);
            }

            var needPos = new Vector3(crd.X, transform.position.y, crd.Z);
            if (smoothly)
            {
                MoveInProcess = true;
                StartCoroutine(MoverHelper.MoveOverSeconds(transform, needPos, speed,
                    () => { MoveInProcess = false; }));
                model.eulerAngles = new Vector3(0, DirectionHelper.GetRotationByDirection(direction), 0);
            }
            else
            {
                transform.position = needPos;
            }
        }

        public void Action()
        {
            connector.TryConnecting();
        }
    }
}