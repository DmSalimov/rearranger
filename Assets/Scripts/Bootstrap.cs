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
        _gameMap = new GameMap();
        LoadLevel(_gameMap);
        _gameMap.TryRegister(new Coordinate(0, 0), player);


        // Init
        controller.Init(player, _gameMap);

        // Test Init
        _gameMap.TryRegister(new Coordinate(1, 1), box);
    }

    private void LoadLevel(GameMap gameMap)
    {
        LevelConfig level1 = new LevelConfig();
        level1.levelNumber = 1;
        level1.level = level1Json.text;
        LevelConfigModel levelConfig = JsonUtility.FromJson<LevelConfigModel>(level1.level);


        foreach (var item in levelConfig.items)
        {
            var coordinate = new Coordinate(item.x, item.z);
            switch (item.type)
            {
                case LevelItemType.Player:
                    gameMap.TryRegister(coordinate, player);
                    break;
                case LevelItemType.Box:
                    gameMap.TryRegister(coordinate, box);
                    break;
                case LevelItemType.Platform:
                    var obj = Instantiate(platformPrefab,
                        new Vector3(coordinate.X, platformPrefab.transform.position.y, coordinate.Z),
                        Quaternion.identity);
                    gameMap.TryRegisterFloor(coordinate, obj.GetComponent<Platform>());
                    break;
            }
        }
    }

}