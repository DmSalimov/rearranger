using UnityEngine;

public class Wall : MonoBehaviour, ICellObject
{
    private Coordinate _coordinate;

    public bool TryMove(ICellObject whom)
    {
        return false;
    }

    public void SetCoordinate(Coordinate coordinate, bool smoothly = false)
    {
        _coordinate = coordinate;
        transform.position = new Vector3(coordinate.X, transform.position.y, coordinate.Z);
    }


    public Coordinate GetCoordinate() => _coordinate;
}