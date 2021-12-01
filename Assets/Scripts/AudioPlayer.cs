using System.Collections.Generic;
using Assets.Scripts.Player;
using Jam.Audio;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {
    public AudioClip primaryShoot;
    [Range(0, 1)] public float primaryShootVolume = 1;
    public AudioClip secondaryShoot;
    [Range(0, 1)] public float secondaryShootVolume = 1;
    public AudioClip life;
    [Range(0, 1)] public float lifeVolume = 0.5f;
    public AudioClip coin;
    [Range(0, 1)] public float coinVolume;
    public List<AudioClip> punchClips;
    [Range(0, 1)] public float punchVolume;

    private AudioSource _audioSource;
    private Health _health;
    private Shoot _shoot;
    private Fart _fart;
    private Fear _fear;

    private void Start() {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _health = GetComponent<Health>();
        _shoot = GetComponent<Shoot>();
        _fart = GetComponent<Fart>();
        _fear = GetComponent<Fear>();

        _health.HitEvent += Punch;
        _health.DeathEvent += Death;
        _health.GainLifeEvent += OnGainLife;
        _shoot.Fire += Fire;
        _fart.Fire += Fire;
        _fear.coinEvent += OnCoin;
    }

    private void Punch(int _) {
        _audioSource.volume = punchVolume;
        _audioSource.clip = punchClips[Random.Range(0, punchClips.Count)];
        _audioSource.Play();
    }

    private static void Death() {
        SoundManager.instance.PlayDeath();
    }

    private void Fire(int fireType) {
        switch (fireType) {
            case 0:
                _audioSource.volume = primaryShootVolume;
                _audioSource.clip = primaryShoot;
                break;
            case 1:
                _audioSource.volume = secondaryShootVolume;
                _audioSource.clip = secondaryShoot;
                break;
        }
        _audioSource.Play();
    }

    private void OnCoin() {
        PlayCoin();
    }

    private void OnGainLife(int _) {
        PlayNewLife();
    }

    private void PlayCoin() {
        _audioSource.volume = coinVolume;
        _audioSource.clip = coin;
        _audioSource.Play();
    }

    private void PlayNewLife() {
        _audioSource.volume = lifeVolume;
        _audioSource.clip = life;
        _audioSource.Play();
    }
}