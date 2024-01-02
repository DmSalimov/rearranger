using System.Collections.Generic;

public class BasePlaceGenerator: IGenerator
{
    public Dictionary<Coordinate, ICellObject> Generate(int sizeX, int sizeZ)
    {
        var map = new Dictionary<Coordinate, ICellObject>();
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                map.Add(new Coordinate(x, z), null);
            }
        }
        return map;
    }
}