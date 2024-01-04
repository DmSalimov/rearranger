using System.Collections.Generic;
using Cells;
using Cells.Intrfeces;
using UnityEngine;

public class GameMap
{
    private Dictionary<Coordinate, Cell> _map;
    private Dictionary<Coordinate, Cell> _floor;
    private List<IActionable> _actionables;
    public IReadOnlyDictionary<Coordinate, Cell> Map => _map;
    public IReadOnlyDictionary<Coordinate, Cell> Floor => _floor;
    public IReadOnlyList<IActionable> Actionables => _actionables;

    public GameMap()
    {
        _map = new Dictionary<Coordinate, Cell>();
        _floor = new Dictionary<Coordinate, Cell>();
        _actionables = new List<IActionable>();
    }

    public void Clear()
    {
        _map.Clear();
        _floor.Clear();
        _actionables.Clear();
    }

    public void RegisterMove(Coordinate from, Coordinate to)
    {
        _map[to] = _map[from];
        _map.Remove(from);
        _map[to]?.SetCoordinate(to, true);
        // _map[from] = null;
    }

    public void RegisterActionable(IActionable item)
    {
        _actionables.Add(item);
    }

    public bool TryRegisterMap(Coordinate coordinate, Cell cell) => TryRegister(_map, coordinate, cell);
    public bool TryRegisterFloor(Coordinate coordinate, Cell cell) => TryRegister(_floor, coordinate, cell);

    private bool TryRegister(Dictionary<Coordinate, Cell> where, Coordinate coordinate, Cell cell)
    {
        where.TryGetValue(coordinate, out Cell whoInCoordinate);
        if (whoInCoordinate == null)
        {
            where[coordinate] = cell;
            cell.SetCoordinate(coordinate);
            return true;
        }

        Debug.Log("TryRegister:: ERROR:: " + coordinate.X + " " + coordinate.Z + " " + cell.GetCellType().ToString());

        return false;
    }
}