using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jam.Areas;

namespace Jam.Entities
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance { get; private set; }
        [SerializeField] private GameObject enemyPrefab;
        private string[] enemyPrefabLocations = new string[4];

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
            // DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            enemyPrefabLocations[0] = "Prefabs/Larvae Enemy";
            enemyPrefabLocations[1] = "Prefabs/Minispider Enemy";
            enemyPrefabLocations[2] = "Prefabs/Spider Enemy";
            enemyPrefabLocations[3] = "Prefabs/Fly Enemy";
        }



        public GameObject SpawnEnemy(WallOrientation exitSide)
        {
            int randomIndex = Random.Range(0, 4);
            enemyPrefab = Resources.Load<GameObject>(enemyPrefabLocations[randomIndex]);

            float xPos;
            float yPos;

            switch (exitSide)
            {
                case WallOrientation.Left:
                    xPos = Random.Range(-5f, 3.5f);
                    yPos = Random.Range(-3f, 3f);
                    break;
                case WallOrientation.Right:
                    xPos = Random.Range(-3.5f, 5f);
                    yPos = Random.Range(-3f, 3f);
                    break;
                case WallOrientation.Top:
                    xPos = Random.Range(-5f, 5f);
                    yPos = Random.Range(-1.5f, 3f);
                    break;
                case WallOrientation.Bottom:
                    xPos = Random.Range(-5f, 5f);
                    yPos = Random.Range(-3f, 1.5f);
                    break;
                default:
                    xPos = 0f;
                    yPos = 0f;
                    break;
            }

            Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            return enemy;
        }

    }
}