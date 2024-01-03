using System;
using Helpers;
using Level;
using UnityEngine;

namespace Cells
{
    public class Player: Cell, IMove
    {
        public override LevelItemType GetCellType() => LevelItemType.Player;

        [SerializeField] private Transform model;

        public bool MoveInProcess { get; private set; }

        public bool TryMove(IMove whom, Cell floorInDirection)
        {
            switch (whom)
            {
                case Player player:
                case Box box:
                case Trailer trailer:
                case null:
                    return true;
                default:
                    return false;
            }
        }

        public override void SetCoordinate(Coordinate crd, bool smoothly = false)
        {
            Vector2 direction = new Vector2(coordinate.X - crd.X, coordinate.Z - crd.Z);
            coordinate = crd;
            var needPos = new Vector3(crd.X, transform.position.y, crd.Z);
            if (smoothly)
            {
                MoveInProcess = true;
                StartCoroutine(MoverHelper.MoveOverSeconds(transform, needPos, speed, () =>
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


       
    }
}