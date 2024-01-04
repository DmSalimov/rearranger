using Cells.Intrfeces;
using Helpers;
using UnityEngine;

namespace Cells.Components
{
    [RequireComponent(typeof(Cell))]
    public class Connector : MonoBehaviour, IConnectable
    {
        [SerializeField] private bool isConnected;


        [SerializeField] private GameObject connectObject;
        [SerializeField] private GameObject lastObject;


        private GameMap _gameMap;
        protected Cell cell;

        private IConnectable _trailer = null;

        public void Init(GameMap gameMap)
        {
            _gameMap = gameMap;
        }

        private void Start()
        {
            cell = GetComponent<Cell>();
        }

        void Update()
        {
            connectObject.SetActive(IsConnected());
            lastObject.SetActive(IsLast());
        }

        public bool IsConnected() => isConnected;
        public bool IsLast() => IsConnected() && _trailer == null;

        public void ConnectEvent()
        {
            isConnected = true;
        }
        public IConnectable GetTrailer() => _trailer;
        public Coordinate GetCoordinate() => cell.GetCoordinate();

        public void TryConnecting()
        {
            if (IsLast())
            {
                var up = TryConnectingOnDirection(Direction.Up());
                if (!up)
                {
                    var right = TryConnectingOnDirection(Direction.Right());
                    if (!right)
                    {
                        var down = TryConnectingOnDirection(Direction.Down());
                        if (!down)
                        {
                            TryConnectingOnDirection(Direction.Left());
                        }
                    }
                }
            }
        }

        private bool TryConnectingOnDirection(Direction direction)
        {
            var map = _gameMap.Map;
            var have = map.TryGetValue(GetCoordinateWithDirection(direction), out Cell cellOnDir);
            if (have)
            {
                var connectable = cellOnDir.GetComponent<IConnectable>();
                if (connectable != null)
                {
                    if (!connectable.IsConnected())
                    {
                        _trailer = connectable;
                        connectable.ConnectEvent();
                        return true;
                    }
                }
            }
           

            return false;
        }

        private Coordinate GetCoordinateWithDirection(Direction direction) =>
            new(cell.GetCoordinate().X + direction.X, cell.GetCoordinate().Z + direction.Z);
    }
}