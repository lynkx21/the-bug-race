using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Enemies;

namespace Assets.Scripts.Player {
    public class Fear : MonoBehaviour {
        public float maxFear = 10f;
        public float minFear = 0f;
        public float currentFear;
        public float increaseRate = 1f;
        public List<GameObject> enemies = new List<GameObject>(); // FIXME: This should not be fixed size or passed by editor
        public event Action<float, float> fearEvent;
        public event Action coinEvent;
        public event Action noEnemiesLeftEvent;

        private void Start() {
            enemies.Clear();
            fearEvent?.Invoke(minFear, maxFear);
        }

        private void Update() {
            // Brutto :(
            enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();

            if (enemies.Count > 0)
                CheckCloseness();
            else
            {
                noEnemiesLeftEvent?.Invoke();
            }

            if (currentFear > maxFear) 
                Mathf.Clamp(currentFear, minFear, maxFear);

            // Update Fear UI
            fearEvent?.Invoke(currentFear, maxFear);
        }

        public void AddEnemyToList(ref GameObject enemy)
        {
            enemies.Add(enemy);
        }

        private void CheckCloseness() {
            List<GameObject> enemiesToRemove = new List<GameObject>();

            foreach (var enemy in enemies) {
                if (enemy != null) {
                    AggroArea enemyAggro = enemy.GetComponentInChildren<AggroArea>();
                    if (!enemyAggro) continue;
                    float enemyAggroLength = enemyAggro.length;
                    float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

                    if (enemyDistance <= enemyAggroLength && currentFear < maxFear) {
                        currentFear += Time.deltaTime * increaseRate;
                    }
                }
                else {
                    enemiesToRemove.Add(enemy);
                }
            }

            if (enemiesToRemove.Count == 0) return;
            foreach (GameObject enemy in enemiesToRemove) {
                enemies.Remove(enemy);
            }

            enemiesToRemove.Clear();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Shit"))
            {   
                coinEvent?.Invoke();
                GainFear();
                Destroy(other.gameObject);
            }
        }

        private void GainFear()
        {
            currentFear += 2f;
            fearEvent?.Invoke(currentFear, maxFear);
        }
    }
}