using Helpers;
using Level;
using UnityEngine;

namespace Cells
{
    public abstract class Cell: MonoBehaviour
    {
        
        [SerializeField] protected float speed = 0.1f;
        
        protected Coordinate coordinate = new Coordinate(0, 0);

        public virtual void SetCoordinate(Coordinate crd, bool smoothly = false)
        {
            coordinate = crd;
            var needPos = new Vector3(crd.X, transform.position.y, crd.Z);
            if (smoothly)
            {
                
                StartCoroutine(MoverHelper.MoveOverSeconds(transform, needPos, speed, null));
            }
            else
            {
                transform.position = needPos;
            }
        }
        public Coordinate GetCoordinate() => coordinate;

        public abstract LevelItemType GetCellType();
    }
}