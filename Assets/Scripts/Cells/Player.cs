using System;
using Helpers;
using Level;
using UnityEngine;

namespace Cells
{
    public class Player: Cell, IMove
    {
        public override LevelItemType GetType() => LevelItemType.Player;

        [SerializeField] private Transform model;

        public bool MoveInProcess { get; private set; }

        public bool TryMove(IMove whom)
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
                    Debug.Log("Необходимо обработать новые объекты");
                    return false;
            }
        }

        public override void SetCoordinate(Coordinate crd, bool smoothly = false)
        {
            Debug.Log("Player:: SetCoordinate");
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
                model.eulerAngles = new Vector3(0, GetRotationByDirection(direction), 0);
                // StartCoroutine(MoverHelper.RotateOverSeconds(model,
                //     new Vector3(0, GetRotationByDirection(direction), 0), 0.1f,
                //     null));
            }
            else
            {
                transform.position = needPos;
            }
        }


        private float GetRotationByDirection(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            return angle < 0 ? angle+360: angle;
        }
    }
}