using UnityEngine;

public class Bootstrap : MonoBehaviour
{

    private bool _isNewGame = true;
    private GameMap _gameMap;
    
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Controller controller;
    [SerializeField] private Player player;
    [SerializeField] private Box box;
    void Start()
    {
        // Generate or Load Game
        if (_isNewGame)
        {
            var basePlaceGenerator = new BasePlaceGenerator();

            var sizeX = 5;
            var sizeZ = 5;
            _gameMap = new GameMap(sizeX, sizeZ, basePlaceGenerator);
            InstantiateMaps();
            // RegAndInstantiateWalls(sizeX, sizeZ);
            _gameMap.TryRegister(new Coordinate(0, 0), player);
        }
        // Init
        controller.Init(player, _gameMap);
        
        // Test Init
        _gameMap.TryRegister(new Coordinate(1, 1), box);
    }

    private void InstantiateMaps()
    {
        foreach (var (coordinate, cell) in _gameMap.Map)
        {
            Instantiate(cellPrefab, new Vector3(coordinate.X, cellPrefab.transform.position.y, coordinate.Z),
                Quaternion.identity);
        }
    }

    private void RegAndInstantiateWalls(int sizeX, int sizeZ)
    {
        for (int x = -1; x < sizeX+1; x++)
        {
            var wall1 = Instantiate(wallPrefab);
            _gameMap.TryRegister(new Coordinate(x, -1), wall1.GetComponent<Wall>());
            var wall2 = Instantiate(wallPrefab);
            _gameMap.TryRegister(new Coordinate(x, sizeZ), wall2.GetComponent<Wall>());
        }
        for (int z = 0; z < sizeZ; z++)
        {
            var wall1 = Instantiate(wallPrefab);
            _gameMap.TryRegister(new Coordinate(-1, z), wall1.GetComponent<Wall>());
            var wall2 = Instantiate(wallPrefab);
            _gameMap.TryRegister(new Coordinate(sizeX, z), wall2.GetComponent<Wall>());
        }
    }

}
