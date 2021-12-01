using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Fart: Shoot
    {
        public override event Action<int> Fire;

        protected override void ShootBullet()
        {
            playerFearComponent.currentFear -= shootCost;
            Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            Fire?.Invoke(1);
        }


        /*
        public GameObject anus;
        public GameObject fart;
        public float reloadTime = 0.5f;
        public float fartCost = 3f;

        private float _currentReloadTime;
        private Fear playerFearComponent;

        public event Action DoFart;

        private void Awake()
        {
            playerFearComponent = GetComponent<Fear>();
        }

        private void Update()
        {
            _currentReloadTime += Time.deltaTime;
            if (Input.GetAxisRaw("Fire2") > 0 && _currentReloadTime > reloadTime && playerFearComponent.currentFear >= fartCost)
            {
                Debug.Log("Fart!");
                playerFearComponent.currentFear -= fartCost;
                GameObject go = Instantiate(fart, anus.transform.position, Quaternion.identity);
                _currentReloadTime = 0;
                DoFart?.Invoke();
            }
        }
        */
    }
}