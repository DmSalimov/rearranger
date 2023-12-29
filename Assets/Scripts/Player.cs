using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, ICellObject
{
    private Coordinate _coordinate;

    [SerializeField] private float speed = 0.1f;

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
        var needPos = new Vector3(coordinate.X, transform.position.y, coordinate.Z);
        if (smoothly)
        {
            StartCoroutine(MoveOverSeconds(needPos, speed, () =>
            {
                _coordinate = coordinate;
            }));
        }
        else
        {
            transform.position = needPos;
            _coordinate = coordinate;
        }
       
    }

    public Coordinate GetCoordinate()
    {
        return _coordinate;
    }

    public IEnumerator MoveOverSeconds(Vector3 end, float seconds, Action callback)
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
        callback();
    }
}