using System;
using Helpers;
using UnityEngine;

namespace Cells
{
    public class Player: BaseCell, ICellObject
    {

        [SerializeField] private Transform model;

        public bool MoveInProcess { get; private set; }

        public bool TryMove(ICellObject whom)
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
                    throw new ArgumentException("Необходимо обработать новые объекты");
            }
        }

        public new void SetCoordinate(Coordinate crd, bool smoothly = false)
        {
            Vector2 direction = new Vector2(coordinate.X - crd.X, coordinate.Z - crd.Z);
            coordinate = crd;
            var needPos = new Vector3(crd.X, transform.position.y, crd.Z);
            if (smoothly)
            {
                model.eulerAngles = new Vector3(0, GetRotationByDirection(direction), 0);
                // model.Rotate(0, 90, 0);
                MoveInProcess = true;
                StartCoroutine(MoverHelper.MoveOverSeconds(transform, needPos, speed, () =>
                {
                    MoveInProcess = false;
                }));
            }
            else
            {
                transform.position = needPos;
            }
        }

        private float GetRotationByDirection(Vector2 direction)
        {
            return Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }
    }
}