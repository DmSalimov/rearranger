using System.Collections.Generic;
using Cells;
using Controllers;
using UnityEngine;
using Level;
using Managers;

public class Bootstrap : MonoBehaviour
{
    [Header("Prefabs")] [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject trailerPrefab;

    [Header("System")] [SerializeField] private Controller controller;
    [SerializeField] private Player player;
    [Header("Levels")] [SerializeField] private TextAsset level1Json;


    [Header("Tests")] [SerializeField] private Box box;


    private MoveManager _moveManager;

    void Start()
    {
        
        // Init
        GameMap.Init();
        _moveManager = new MoveManager();
        controller.Init(player, _moveManager);
        player.Init(_moveManager);
        
        // Load
        LoadLevel(1);
    }

    public void RestartLevel(int levelNumber)
    {
        GameMap.Clear();
        LoadLevel(levelNumber);
    }

    private void LoadLevel(int levelNumber)
    {
        LevelConfig level1 = new LevelConfig();
        level1.levelNumber = 1;
        level1.level = level1Json.text;
        LevelConfigModel levelConfig = JsonUtility.FromJson<LevelConfigModel>(level1.level);

        RegisterGameMap(levelConfig.items);
    }

    private void RegisterGameMap(List<LevelItemModel> items)
    {
        GameMap.RegisterActionable(player);
        foreach (var item in items)
        {
            var coordinate = new Coordinate(item.x, item.z);
            switch (item.type)
            {
                case LevelItemType.Player:
                    GameMap.TryRegisterMap(coordinate, player);
                    break;
                case LevelItemType.Box:
                    // Test Init -> TODO:: Instantiate box
                    GameMap.TryRegisterMap(coordinate, box);
                    break;
                case LevelItemType.Platform:
                    var obj = Instantiate(platformPrefab,
                        new Vector3(coordinate.X, platformPrefab.transform.position.y, coordinate.Z),
                        Quaternion.identity);
                    GameMap.TryRegisterFloor(coordinate, obj.GetComponent<Platform>());
                    break;
                case LevelItemType.Trailer:
                    var trailerGameObject = Instantiate(trailerPrefab,
                        new Vector3(coordinate.X, trailerPrefab.transform.position.y, coordinate.Z),
                        Quaternion.identity);
                    var trailer = trailerGameObject.GetComponent<Trailer>();
                    trailer.Init();
                    GameMap.TryRegisterMap(coordinate, trailer);
                    GameMap.RegisterActionable(trailer);
                    break;
            }
        }
    }
}