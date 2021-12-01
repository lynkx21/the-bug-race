using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jam.Architecture
{
    public class AppManager : MonoBehaviour
    {
        private AppManager Instance;
        [SerializeField] private TitleManager titleManager;

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
            DontDestroyOnLoad(this.gameObject);


            titleManager = FindObjectOfType<TitleManager>();
        }

        private void Update()
        {
            if (Input.GetAxisRaw("Fire1") > 0 && SceneManager.GetActiveScene().buildIndex == 0 && Time.timeScale > 0)
            {
                titleManager.LoadGame();
            }
        }
    }
}