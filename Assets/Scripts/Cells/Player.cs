using Cells.Intrfeces;
using Helpers;
using Level;
using Managers;
using UnityEngine;

namespace Cells
{
    public class Player : Cell, IMovable, IConnectable
    {
        public override LevelItemType GetCellType() => LevelItemType.Player;

        [SerializeField] private Transform model;

        public bool MoveInProcess { get; private set; }
        private IConnectable _trailer = null;
        private IMover _mover;
        
        public void Init(IMover mover)
        {
            _mover = mover;
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
            if (!IsLast())
            {
                _mover.Move(_trailer, _trailer.GetCoordinate() , oldCoordinate);
            }
            var needPos = new Vector3(crd.X, transform.position.y, crd.Z);
            if (smoothly)
            {
                MoveInProcess = true;
                StartCoroutine(MoverHelper.MoveOverSeconds(transform, needPos, speed,
                    () =>
                    {
                        MoveInProcess = false;
                    }));
                model.eulerAngles = new Vector3(0, DirectionHelper.GetRotationByDirection(direction), 0);
                // StartCoroutine(MoverHelper.RotateOverSeconds(model,
                //     new Vector3(0, GetRotationByDirection(direction), 0), 0.1f,
                //     null));
               
            }
            else
            {
                transform.position = needPos;
            }
        }


        public bool IsConnected() => true;

        public bool IsLast() => _trailer == null;

        public void Connect(IConnectable who)
        {
            _trailer = who;
            _trailer.Joined(this);
        }

        public void Joined(IConnectable head)
        {
        }

        public IConnectable GetTrailer() => _trailer;
    }
}