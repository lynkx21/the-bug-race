using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using Assets.Scripts.Player;

namespace Assets.Scripts.Enemies {
    public class Enemy : MonoBehaviour {
        public int strength = 20;
        public int projectileEffectiveness = 10;
        public HealthBar healthBar;
        public int difficulty;

        private Vector3 _offset;
        
        private void Start() {
            healthBar.setMaxHealth(strength);
            healthBar.setHealth(strength);
            _offset = healthBar.transform.position - transform.position;
        }

        private void Update() {
            healthBar.transform.position = transform.position + _offset;
        }

        public void OnTriggerEnter2D(Collider2D other) {
            
            if (other.gameObject.CompareTag("Projectile")) {
                healthBar.TakeDamage(projectileEffectiveness);
                if (healthBar.getHealth() <= 0)
                    Destroy(transform.parent.gameObject);
            }
        }
    }
}