using System.Collections.Generic;
using Cells;
using Cells.Intrfeces;
using UnityEngine;

public static class GameMap
{
    private static Dictionary<Coordinate, Cell> _map = new Dictionary<Coordinate, Cell>();
    private static Dictionary<Coordinate, Cell> _floor = new Dictionary<Coordinate, Cell>();
    private static List<IActionable> _actionables = new List<IActionable>();
    public static IReadOnlyDictionary<Coordinate, Cell> Map => _map;
    public static IReadOnlyDictionary<Coordinate, Cell> Floor => _floor;
    public static IReadOnlyList<IActionable> Actionables => _actionables;

    public static void Init()
    {
        Clear();
    }

    public static void Clear()
    {
        _map.Clear();
        _floor.Clear();
        _actionables.Clear();
    }

    public static void RegisterMove(Coordinate from, Coordinate to)
    {
        _map[to] = _map[from];
        _map.Remove(from);
        _map[to]?.SetCoordinate(to, true);
        // _map[from] = null;
    }

    public static void RegisterActionable(IActionable item)
    {
        _actionables.Add(item);
    }

    public static bool TryRegisterMap(Coordinate coordinate, Cell cell) => TryRegister(_map, coordinate, cell);
    public static bool TryRegisterFloor(Coordinate coordinate, Cell cell) => TryRegister(_floor, coordinate, cell);

    private static bool TryRegister(Dictionary<Coordinate, Cell> where, Coordinate coordinate, Cell cell)
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