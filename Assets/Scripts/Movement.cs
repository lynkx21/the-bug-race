using System;
using UnityEngine;

namespace Assets.Scripts.Player {
    public class Movement : MonoBehaviour {
        public float speed = 5f;
        public Vector3 currentDirection;
        public Vector3 facingDirection = Vector3.up;
        public bool canMove = true;
        public float rotationSpeed = 0.1f;

        // Update is called once per frame
        private void Update() {
            if (!canMove) return;

            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");
            currentDirection = Vector3.zero;
            if (horizontalMovement != 0) {
                currentDirection += Vector3.right * horizontalMovement;
            }

            if (verticalMovement != 0) {
                currentDirection += Vector3.up * verticalMovement;
            }
            
            transform.position += Time.deltaTime * speed * currentDirection.normalized;

            if (currentDirection != Vector3.zero) {
                facingDirection = currentDirection;
                float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90f;
                var desiredRotation = Quaternion.Euler(Vector3.forward * angle);
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 
                    (Time.time % Time.deltaTime) / Time.deltaTime * rotationSpeed);
            }
            // transform.Rotate(Vector3.forward * angle);
            // transform.Rotate(Vector3.forward);
            // transform.LookAt(transform.position + Vector3.right);
            // transform.LookAt(transform.position + direction);
        }
    }
}