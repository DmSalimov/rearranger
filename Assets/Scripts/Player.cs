using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, ICellObject
{
    private Coordinate _coordinate = new Coordinate(0,0);

    [SerializeField] private float speed = 0.1f;
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
            case Wall wall:
                return false;
            case null:
                return true;
            default:
                throw new ArgumentException("Необходимо обработать новые объекты");
        }
    }

    public void SetCoordinate(Coordinate coordinate, bool smoothly = false)
    {
        Vector2 direction = new Vector2(_coordinate.X - coordinate.X, _coordinate.Z - coordinate.Z);
        _coordinate = coordinate;
        var needPos = new Vector3(coordinate.X, transform.position.y, coordinate.Z);
        if (smoothly)
        {
            model.eulerAngles = new Vector3(0 ,GetRotationByDirection(direction),0);
            // model.Rotate(0, 90, 0);
            StartCoroutine(MoveOverSeconds(needPos, speed));
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

    public Coordinate GetCoordinate()
    {
        return _coordinate;
    }

    public IEnumerator MoveOverSeconds(Vector3 end, float seconds)
    {
        MoveInProcess = true;
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startingPos, end, elapsedTime / seconds);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = end;
        MoveInProcess = false;
    }
}