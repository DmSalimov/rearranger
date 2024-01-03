using System;
using System.Collections.Generic;
using Cells;
using UnityEngine;
using Level;


public class GameMap : IMover
{
    private Dictionary<Coordinate, Cell> _map;
    private Dictionary<Coordinate, Cell> _floor;
    public IReadOnlyDictionary<Coordinate, Cell> Map => _map;
    public IReadOnlyDictionary<Coordinate, Cell> Floor => _floor;

    private int _sizeX;
    private int _sizeZ;

    public GameMap()
    {
        _map = new Dictionary<Coordinate, Cell>();
        _floor = new Dictionary<Coordinate, Cell>();
    }

    public bool TryMove(IMove who, Direction direction)
    {
        var from = who.GetCoordinate();
        var to = new Coordinate(from.X + direction.X, from.Z + direction.Z);
        var toInArea = Floor.TryGetValue(to, out Cell floorInDirection);
        Map.TryGetValue(to, out Cell whoInDirection);
        IMove whoInDirectionMove = whoInDirection as IMove;

        if (toInArea)
        {
            if (whoInDirection != null)
            {
                if (whoInDirectionMove != null)
                {
                    var canMoveAccess = who.TryMove(whoInDirectionMove);

                    if (canMoveAccess)
                    {
                        var canPush = TryMove(whoInDirectionMove, direction);
                        if (!canPush)
                        {
                            Debug.Log("TryMove::false canPush");
                            return false;
                        }

                        RegisterMove(from, to);
                        return true;
                    }

                    Debug.Log(String.Format("TryMove::canMoveAccess:: {0}", canMoveAccess));
                }
            }
            else
            {
                RegisterMove(from, to);
                return true;
            }
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

    public bool TryRegister(Coordinate coordinate, Cell cell)
    {
        _map.TryGetValue(coordinate, out Cell whoInCoordinate);
        if (whoInCoordinate == null)
        {
            _map[coordinate] = cell;
            cell.SetCoordinate(coordinate);
            return true;
        }

        Debug.Log("TryRegister:: ERROR:: " + coordinate.X + " " + coordinate.Z + " " + cell.GetType().ToString());

        return false;
    }
    public bool TryRegisterFloor(Coordinate coordinate, Cell cell)
    {
        _floor.TryGetValue(coordinate, out Cell whoInCoordinate);
        if (whoInCoordinate == null)
        {
            _floor[coordinate] = cell;
            cell.SetCoordinate(coordinate);
            return true;
        }

        Debug.Log("TryRegister:: ERROR:: " + coordinate.X + " " + coordinate.Z + " " + cell.GetType().ToString());

        return false;
    }
}