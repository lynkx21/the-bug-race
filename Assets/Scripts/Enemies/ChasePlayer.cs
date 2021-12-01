using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemies {
    public class ChasePlayer : MonoBehaviour {
        public float chasingSpeed = 2f;
        public bool chasePlayerOnlyWhenNotMoving;
        private ThePlayer _player;
        private Movement _playerMovement;

        private void Start() {
            _player = ThePlayer.Instance;
            _playerMovement = _player.GetComponent<Movement>();
        }

        private void Update() {
            var targetDirection = (_player.transform.position - transform.position).normalized;
            transform.up = targetDirection;
            
            if (chasePlayerOnlyWhenNotMoving && _playerMovement.canMove) return;
            transform.position += targetDirection * chasingSpeed * Time.deltaTime;
        }
    }
}