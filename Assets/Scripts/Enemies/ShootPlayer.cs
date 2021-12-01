using System;
using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts.Enemies {
    public class ShootPlayer : MonoBehaviour {

        public GameObject bullet;
        public GameObject gun;
        public float reloadTime = 5f;
        
        public event Action Fire;

        private float _lastShotTimestamp;

        private void Start() {
            _lastShotTimestamp = Time.time;
        }

        private void Update() {
            if (Time.time - _lastShotTimestamp > reloadTime) {
                Shoot();
                _lastShotTimestamp = Time.time;
            }
        }


        private void Shoot() {
            Fire?.Invoke();
            var go = Instantiate(bullet, gun.transform.position, Quaternion.identity);
            var bulletComponent = go.GetComponent<Bullet>();
            bulletComponent.Direction = (ThePlayer.Instance.transform.position - gun.transform.position).normalized;
        }
    }
}