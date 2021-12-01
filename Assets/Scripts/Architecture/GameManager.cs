using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Jam.Areas;
using Jam.Entities;
using Assets.Scripts.Player;
using Assets.Scripts.Enemies;
using Jam.Audio;

namespace Jam.Architecture
{
    public class GameManager : MonoBehaviour
    {
        // Singleton
        public static GameManager Instance { get; private set; }
        public static GameObject World { get; private set; }
        public static GameObject EnemySpawner { get; private set; }
        
        [SerializeField] private GameObject playerPrefab;
        public GameObject player;
        private Fear playerFearComponent;

        public event Action roomClearCaller;
        public event Action<int> roomChangeEvent;

        [SerializeField] private GameObject mainUiPrefab;
        public GameObject mainUi;
        private UIManager mainUiManager;

        public static int roomsSurvived;

        [SerializeField] private GameObject shitPrefab;
        [SerializeField] private float shitProbability = 0.25f;

        private WallOrientation exitSide;

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

            World = CreateWorld();
            EnemySpawner = CreateEnemySpawner();

            player = Instantiate(playerPrefab, this.transform);
            mainUi = Instantiate(mainUiPrefab, this.transform);

            roomsSurvived = 0;
        }

        private void Start()
        {
            playerFearComponent = player.GetComponent<Fear>();
            playerFearComponent.noEnemiesLeftEvent += OnNoEnemiesLeft;

            mainUiManager = mainUi.GetComponent<UIManager>();
            mainUiManager.player = player;
        }

        private GameObject CreateWorld(string name = null)
        {
            if (name == null)
            {
                name = "World";
            }
            GameObject world = new GameObject(name);
            world.AddComponent<World>();
            world.GetComponent<World>().exitRoomEvent += OnExitRoom;
            world.GetComponent<World>().enterRoomEvent += OnEnterRoom;
            roomClearCaller += world.GetComponent<World>().OnClearedRoom;
            return world;
        }

        private GameObject CreateEnemySpawner()
        {
            GameObject enemySpawner = new GameObject("Enemy Spawner");
            enemySpawner.AddComponent<EnemySpawner>();
            return enemySpawner;
        }

        private void OnNoEnemiesLeft()
        {
            roomClearCaller?.Invoke();
        }

        private void OnExitRoom(WallOrientation exitSide)
        {
            switch (exitSide)
            {
                case WallOrientation.Left:
                    player.transform.position = Vector3.right * 6f;
                    player.transform.rotation = Quaternion.Euler(Vector3.forward * 90f);
                    break;
                case WallOrientation.Right:
                    player.transform.position = Vector3.left * 6f;
                    player.transform.rotation = Quaternion.Euler(Vector3.back * 90f);
                    break;
                case WallOrientation.Top:
                    player.transform.position = Vector3.down * 3f;
                    break;
                case WallOrientation.Bottom:
                    player.transform.position = Vector3.up * 3f;
                    player.transform.rotation = Quaternion.Euler(Vector3.forward * 180f);
                    break;
                default:
                    break;
            }

            this.exitSide = exitSide;
        }

        private void OnEnterRoom(Vector2 worldCoordinates)
        {
            ClearMovingParts();  // to remove all moving components
            
           
            
            roomsSurvived += 1;
            roomChangeEvent?.Invoke(roomsSurvived);
            
            if (roomsSurvived == 1 || roomsSurvived % 3 == 0)
                SoundManager.instance.PlayScream();

            //float distanceFromOrigin = worldCoordinates.magnitude;
            //if (distanceFromOrigin == 0)
            //{
            //    // Note: forcing starting room to have enemies
            //    distanceFromOrigin = 1;
            //}
            //int difficulty = Mathf.FloorToInt(Mathf.Log(distanceFromOrigin + 1, 1.5f));
            // int difficulty = Mathf.FloorToInt(Mathf.Log(roomsSurvived + 1, 1.5f));
            int difficulty = Mathf.FloorToInt(roomsSurvived / 3.0f) + 1;
            EnemySpawner spawner = EnemySpawner.GetComponent<EnemySpawner>();

            int enemiesNumber = difficulty;
            // Debug.Log($"[Game Manager] Difficulty: {distanceFromOrigin:0.00} -> {difficulty} -> {enemiesNumber}");

            for (int i = 0; i < enemiesNumber; i++)
            {
                spawner.SpawnEnemy(exitSide);
            }

            // Generate shit :)
            GenerateShit();
        }

        private void ClearMovingParts()
        {
            List<GameObject> lives = GameObject.FindGameObjectsWithTag("Life").ToList();
            List<GameObject> shits = GameObject.FindGameObjectsWithTag("Shit").ToList();
            List<GameObject> projectiles = GameObject.FindGameObjectsWithTag("Projectile").ToList();
            List<GameObject> spiderwebs = GameObject.FindGameObjectsWithTag("Spiderweb").ToList();
            List<GameObject> movingParts = new List<GameObject>();
            movingParts.AddRange(lives);
            movingParts.AddRange(shits);
            movingParts.AddRange(projectiles);
            movingParts.AddRange(spiderwebs);

            if (movingParts.Count > 0)
            {
                foreach (GameObject go in movingParts)
                {
                    Destroy(go);
                }
            }
        }

        private void GenerateShit()
        {
            if (UnityEngine.Random.Range(0f, 1f) <= shitProbability)
            {
                float xPos = UnityEngine.Random.Range(-5f, 5f);
                float yPos = UnityEngine.Random.Range(-3f, 3f);
                Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);
                Instantiate(shitPrefab, spawnPosition, Quaternion.identity);
            }
        }

    }
}