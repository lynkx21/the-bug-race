using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float delayTime;
        [SerializeField] private float aggroArea;
        [SerializeField] private float shootDistance;
        [SerializeField] private Transform player;
        [SerializeField] private Transform face;

        private void Start()
        {
         }

        private void Update()
        {
            AimTarget();
        }

        private void AimTarget()
        {
            Vector3 direction = (transform.position - player.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            // float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}