using UnityEngine;

namespace Helpers
{
    public class Direction
    {
        public int X { get; private set; }
        public int Z { get; private set; }

        public Direction(int x, int z)
        {
            X = x > 0 ? 1 : x < 0 ? -1 : 0;
            Z = z > 0 ? 1 : z < 0 ? -1 : 0;
        }

        public static Direction Up() => new Direction(-1, 0);
        public static Direction Down() => new Direction(1, 0);
        public static Direction Left() => new Direction(0, -1);
        public static Direction Right() => new Direction(0, 1);
    }

    public static class DirectionHelper
    {
        public static float GetRotationByDirection(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            return angle < 0 ? angle + 360 : angle;
        }
    }
}