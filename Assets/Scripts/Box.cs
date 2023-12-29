using System;
using System.Collections;
using UnityEngine;

public class Box: MonoBehaviour, ICellObject
{
    [SerializeField] private float speed = 0.1f;
    
    private Coordinate _coordinate;
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
        _coordinate = coordinate;
        var needPos = new Vector3(coordinate.X, transform.position.y, coordinate.Z);
        if (smoothly)
        {
            
            StartCoroutine(MoveOverSeconds(gameObject, needPos, speed));
        }
        else
        {
            transform.position = needPos;
        }

       
    }
    
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = end;
    }

    public Coordinate GetCoordinate()
    {
        return _coordinate;
    }
}