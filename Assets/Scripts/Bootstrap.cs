using System.Collections.Generic;
using Cells;
using Controllers;
using UnityEngine;
using Level;
using Managers;

public class Bootstrap : MonoBehaviour
{
    private GameMap _gameMap;

    [Header("Prefabs")] [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject trailerPrefab;

    [Header("System")] [SerializeField] private Controller controller;
    [SerializeField] private Player player;
    [Header("Levels")] [SerializeField] private TextAsset level1Json;


    [Header("Tests")] [SerializeField] private Box box;


    private MoveManager _moveManager;

    void Start()
    {
        _gameMap = new GameMap();
        _moveManager = new MoveManager(_gameMap);

        LoadLevel(_gameMap);

        // Init
        controller.Init(player, _moveManager);
        player.Init(_moveManager, _gameMap);
    }

    private void LoadLevel(GameMap gameMap)
    {
        LevelConfig level1 = new LevelConfig();
        level1.levelNumber = 1;
        level1.level = level1Json.text;
        LevelConfigModel levelConfig = JsonUtility.FromJson<LevelConfigModel>(level1.level);

        RegisterItems(gameMap, levelConfig.items);
    }

    private void RegisterItems(GameMap gameMap, List<LevelItemModel> items)
    {
        foreach (var item in items)
        {
            var coordinate = new Coordinate(item.x, item.z);
            switch (item.type)
            {
                case LevelItemType.Player:
                    gameMap.TryRegisterMap(coordinate, player);
                    break;
                case LevelItemType.Box:
                    // Test Init -> TODO:: Instantiate box
                    gameMap.TryRegisterMap(coordinate, box);
                    break;
                case LevelItemType.Platform:
                    var obj = Instantiate(platformPrefab,
                        new Vector3(coordinate.X, platformPrefab.transform.position.y, coordinate.Z),
                        Quaternion.identity);
                    gameMap.TryRegisterFloor(coordinate, obj.GetComponent<Platform>());
                    break;
                case LevelItemType.Trailer:
                    var trailerGameObject = Instantiate(trailerPrefab,
                        new Vector3(coordinate.X, trailerPrefab.transform.position.y, coordinate.Z),
                        Quaternion.identity);
                    var trailer = trailerGameObject.GetComponent<Trailer>();
                    trailer.Init(gameMap);
                    gameMap.TryRegisterMap(coordinate, trailer);
                    gameMap.RegisterActionable(trailer);
                    break;
            }
        }
    }
}