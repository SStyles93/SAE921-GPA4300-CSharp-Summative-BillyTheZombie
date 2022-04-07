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
    }
}
