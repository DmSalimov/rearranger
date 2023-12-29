using System.Collections.Generic;
using System.Numerics;

public class BaseSpaceShipGenerator: IGenerator
{
    public Dictionary<Coordinate, ICellObject> Generate(int sizeX, int sizeZ)
    {
        var maps = new Dictionary<Coordinate, ICellObject>();
        for (int x = -1; x < sizeX+1; x++)
        {
            for (int z = -1; z < sizeZ+1; z++)
            {
                maps.Add(new Coordinate(x, z), null);
            }
        }
        

        return maps;
    }
}