using Helpers;
using UnityEngine;

namespace Cells
{
    public abstract class BaseCell: MonoBehaviour
    {
        
        [SerializeField] protected float speed = 0.1f;
        
        protected Coordinate coordinate = new Coordinate(0, 0);

        public void SetCoordinate(Coordinate coordinate, bool smoothly = false)
        {
            this.coordinate = coordinate;
            var needPos = new Vector3(coordinate.X, transform.position.y, coordinate.Z);
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
    }
}