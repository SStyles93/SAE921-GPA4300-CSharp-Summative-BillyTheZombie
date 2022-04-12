using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        //Reference Scripts
        private PlayerController _playerController;
        private PlayerStats _playerStats;

        //Reference Components
        private Rigidbody2D _rb;

        private bool _canMove = true;
        public bool CanMove { get => _canMove; set => _canMove = value; }

        void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerStats = GetComponent<PlayerStats>();
            _rb = GetComponent<Rigidbody2D>();

            _playerController.PlayGame += PauseMovement;
        }
        private void Update()
        {
            //Movement
            if (_canMove)
            {
                Vector3 movement = new Vector3(_playerController.Movement.x, _playerController.Movement.y, 0.0f);
                transform.Translate(movement * _playerStats.Speed * Time.deltaTime);
                _rb.constraints = RigidbodyConstraints2D.None;
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        /// <summary>
        /// Stops the movement of the player when the game is not running
        /// </summary>
        /// <param name="isRunning">The state in which we want the player to be (paused/!paused)</param>
        private void PauseMovement(bool isRunning)
        {
            _canMove = isRunning;
        }
    }
}
