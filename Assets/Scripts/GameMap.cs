using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : IMover
{
    private Dictionary<Coordinate, ICellObject> _map;
    public IReadOnlyDictionary<Coordinate, ICellObject> Map => _map;

    private int _sizeX;
    private int _sizeZ;

    public GameMap(int sizeX, int sizeZ, IGenerator generator)
    {
        var map = generator.Generate(sizeX, sizeZ);
        map.Remove(new Coordinate(3, 3));
        _map = new Dictionary<Coordinate, ICellObject>(map);
    }

    public bool TryMove(ICellObject who, Direction direction)
    {
        var from = who.GetCoordinate();
        var to = new Coordinate(from.X + direction.X, from.Z + direction.Z);
        var toInArea = _map.TryGetValue(to, out ICellObject whoInDirection);

        if (toInArea)
        {
            var canMoveAccess = who.TryMove(whoInDirection);

            if (canMoveAccess)
            {
                if (whoInDirection != null)
                {
                    var canPush = TryMove(whoInDirection, direction);
                    if (!canPush)
                    {
                        Debug.Log("TryMove::false canPush");
                        return false;
                    }
                }

                RegisterMove(from, to);
                return true;
            }

            Debug.Log(String.Format("TryMove::canMoveAccess:: {0}", canMoveAccess));
        }

        Debug.Log(String.Format("TryMove::false exit toInArea:: {0}", toInArea));

        return false;
    }

    private void RegisterMove(Coordinate from, Coordinate to)
    {
        _map[to] = _map[from];
        _map[to].SetCoordinate(to, true);
        _map[from] = null;
    }

    public bool TryRegister(Coordinate coordinate, ICellObject cell)
    {
        _map.TryGetValue(coordinate, out ICellObject who);
        if (who == null)
        {
            _map[coordinate] = cell;
            cell.SetCoordinate(coordinate);
            return true;
        }

        Debug.Log("TryRegister:: ERROR:: " + coordinate.X + " " + coordinate.Z + " " + cell.GetType().ToString());

        return false;
    }
}