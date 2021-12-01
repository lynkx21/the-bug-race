using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Shoot : MonoBehaviour
    {
        public GameObject gun;
        public GameObject bulletPrefab;
        public float reloadTime = 0.5f;
        public float shootCost = 1.5f;
        public string shootButton = "Fire1";

        private float _currentReloadTIme;
        protected Fear playerFearComponent;

        public virtual event Action<int> Fire;

        private void Awake()
        {
            playerFearComponent = GetComponent<Fear>();
        }

        private void Update()
        {
            _currentReloadTIme += Time.deltaTime;

            if (Input.GetAxisRaw(shootButton) > 0 &&
                _currentReloadTIme > reloadTime &&
                playerFearComponent.currentFear >= shootCost)
            {
                ShootBullet();
                _currentReloadTIme = 0;
            }
        }

        protected virtual void ShootBullet()
        {
            playerFearComponent.currentFear -= shootCost;
            GenerateBullet();
            Fire?.Invoke(0);
        }

        protected virtual void GenerateBullet()
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.Direction = ThePlayer.Instance.transform.up;
        }
    }
}