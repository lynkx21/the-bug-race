using System;
using UnityEngine;

namespace Assets.Scripts.Player {
    public class Health : MonoBehaviour {
        public int health;

        public event Action<int> HitEvent;
        public event Action<int> GainLifeEvent;
        public event Action DeathEvent;

        private int _maxHealth;

        private void Start() {
            _maxHealth = health;
        }


        public void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Enemy")) {
                if (health > 0) {
                    Hit();
                }

                if (health == 0) {
                    Die();
                }
            }

            if (other.gameObject.CompareTag("Life") && health < _maxHealth) {
                GainLife();
                Destroy(other.gameObject);
            }
            
        }

        private void Hit() {
            health -= 1;
            HitEvent?.Invoke(health);
        }

        private void GainLife() {
            health += 1;
            GainLifeEvent?.Invoke(health);
        }

        private void Die() {
            DeathEvent?.Invoke();
            transform.gameObject.SetActive(false);
        }
    }
}