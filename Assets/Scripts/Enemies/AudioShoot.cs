using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Enemies {
    public class AudioShoot : MonoBehaviour {
        public AudioClip shootClip;
        private AudioSource _audioSource;
        private ShootPlayer _shootPlayerComponent;

        private void Start() {
            _audioSource = GetComponent<AudioSource>();
            _shootPlayerComponent = GetComponent<ShootPlayer>();
            _shootPlayerComponent.Fire += Play;
            _audioSource.clip = shootClip;
        }

        private void Play() {
            _audioSource.Play();
        }
    }
}