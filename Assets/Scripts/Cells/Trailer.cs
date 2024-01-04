using Cells.Components;
using Cells.Intrfeces;
using Helpers;
using Level;
using UnityEngine;

namespace Cells
{
    [RequireComponent(typeof(Connector))]
    public class Trailer : Cell, IMovable, IActionable
    {
        [SerializeField] private Transform model;

        protected Connector connector;

        public void Init()
        {
            connector = GetComponent<Connector>();
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

        public void Action()
        {
            connector.TryConnecting();
        }
    }
}