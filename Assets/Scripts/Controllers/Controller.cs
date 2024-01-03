using Cells;
using Managers;
using UnityEngine;
using Helpers;

namespace Controllers
{
    public class Controller : MonoBehaviour
    {
        private IMover _mover;
        private Player _player;

        public void Init(Player player, IMover mover)
        {
            _player = player;
            _mover = mover;
        }


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!_player.MoveInProcess)
                {
                    var result = _mover.TryMove(_player, new Direction(-1, 0));
                    Debug.Log("W " + result);
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (!_player.MoveInProcess)
                {
                    var result = _mover.TryMove(_player, new Direction(1, 0));
                    Debug.Log("S " + result);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (!_player.MoveInProcess)
                {
                    var result = _mover.TryMove(_player, new Direction(0, 1));
                    Debug.Log("D " + result);
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (!_player.MoveInProcess)
                {
                    var result = _mover.TryMove(_player, new Direction(0, -1));
                    Debug.Log("A " + result);
                }
            }
        }
    }
}