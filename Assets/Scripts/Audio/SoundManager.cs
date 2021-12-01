using UnityEditor.SceneManagement;
using UnityEngine;

namespace Jam.Audio {
    public class SoundManager : MonoBehaviour {
        public static SoundManager instance;

        public AudioClip soundtrack;
        public AudioClip scream;
        public AudioClip playerDeath;
        
        private AudioSource _effectsSource;
        private AudioSource _soundtrackSource;
        private AudioSource _playerDeathSource;

        private void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            if (instance == this)
                return;

            Destroy(gameObject);
        }

        public void Start() {
            // Soundtrack
            _soundtrackSource = gameObject.AddComponent<AudioSource>();
            _soundtrackSource.clip = soundtrack;
            _soundtrackSource.volume = .85f;
            _soundtrackSource.loop = true;
            PlaySoundtrack();
            
            // Effects
            _effectsSource = gameObject.AddComponent<AudioSource>();

            // Player Death
            _playerDeathSource = gameObject.AddComponent<AudioSource>();
        }

        private void PlaySoundtrack() {
            _soundtrackSource.Play();
        }

        public void PlayScream() {
            _effectsSource.clip = scream;
            _effectsSource.Play();
        }
    }
}