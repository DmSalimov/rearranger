using Helpers;
using Level;
using UnityEngine;

namespace Cells
{
    public class Trailer : Cell, IMove
    {
        [SerializeField] private Transform model;

        public override LevelItemType GetCellType() => LevelItemType.Box;

        public bool TryMove(IMove whom, Cell floorInDirection) => false;


        public override void SetCoordinate(Coordinate crd, bool smoothly = false)
        {
            if (smoothly)
            {
                Vector2 direction = new Vector2(coordinate.X - crd.X, coordinate.Z - crd.Z);
                model.eulerAngles = new Vector3(0, DirectionHelper.GetRotationByDirection(direction), 0);
            }

            base.SetCoordinate(crd, smoothly);
        }
    }
}