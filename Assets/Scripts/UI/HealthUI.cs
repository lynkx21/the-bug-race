using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Player;

public class HealthUI : MonoBehaviour {
    [SerializeField] private Sprite heartSprite;
    [SerializeField] private Image deathImage;
    [SerializeField] private TMP_Text exitText;

    private Health playerHealth;
    private readonly List<GameObject> _hearts = new List<GameObject>();
    private GridLayoutGroup _gridLayout;


    private void Awake() {
        _gridLayout = GetComponentInChildren<GridLayoutGroup>();
        playerHealth = ThePlayer.Instance.GetComponent<Health>();
        playerHealth.HitEvent += OnPlayerHit;
        playerHealth.GainLifeEvent += OnPlayerGainedLife;
        playerHealth.DeathEvent += OnPlayerDeath;
        for (int i = 0; i < playerHealth.health; ++i)
            AddHeart();
        deathImage.gameObject.SetActive(false);
        exitText.text = UIHelper.WriteStringToFont("Press Esc to open menu".ToUpper());
        exitText.gameObject.SetActive(false);
    }

    private void SetAlpha(float alpha, ref Image image) {
        var color = image.color;
        color.a = alpha;
        image.color = color;
    }

    private void AddHeart() {
        var go = new GameObject("Heart");
        var rectTransform = go.AddComponent<RectTransform>();
        rectTransform.transform.SetParent(_gridLayout.gameObject.transform);
        var image = go.AddComponent<Image>();
        image.sprite = heartSprite;
        _hearts.Add(go);
    }

    private void OnPlayerHit(int health) {
        // _hearts[health].SetActive(false);
        var image = _hearts[health].GetComponent<Image>();
        SetAlpha(0.25f, ref image);
        
        if (health == 0) {
            playerHealth.HitEvent -= OnPlayerHit;
        }
    }

    private void OnPlayerGainedLife(int health) {
        var image = _hearts[health - 1].GetComponent<Image>();
        SetAlpha(1.0f, ref image);
    }

    private void OnPlayerDeath() {
        deathImage.gameObject.SetActive(true);
        exitText.gameObject.SetActive(true);
        playerHealth.DeathEvent -= OnPlayerDeath;
    }
}