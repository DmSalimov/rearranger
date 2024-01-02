using Cells;
using UnityEngine;
using Level;

public class Bootstrap : MonoBehaviour
{
    private GameMap _gameMap;

    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private Controller controller;
    [SerializeField] private Player player;
    [SerializeField] private Box box;

    [SerializeField] private TextAsset level1Json;

    void Start()
    {
        _gameMap = GetLevel();
        InstantiateMaps();
        _gameMap.TryRegister(new Coordinate(0, 0), player);
        
        
        // Init
        controller.Init(player, _gameMap);

        // Test Init
        _gameMap.TryRegister(new Coordinate(1, 1), box);
    }

    private GameMap GetLevel()
    {
        LevelConfig level1 = new LevelConfig();
        level1.levelNumber = 1;
        level1.level = level1Json.text;
        LevelConfigModel levelConfig = JsonUtility.FromJson<LevelConfigModel>(level1.level);

        return new GameMap(levelConfig);
    }

    private void InstantiateMaps()
    {
        foreach (var (coordinate, cell) in _gameMap.Map)
        {
            Instantiate(platformPrefab, new Vector3(coordinate.X, platformPrefab.transform.position.y, coordinate.Z),
                Quaternion.identity);
        }
    }
}