    using System.Collections.Generic;

    public interface IGenerator
    {
        public Dictionary<Coordinate, ICellObject> Generate(int sizeX, int sizeZ);
    }