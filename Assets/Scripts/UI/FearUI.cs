using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Player;

public class FearUI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Fear fearComponent;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Sprite fearMeterContent;
    [SerializeField] private RectTransform fearLevel;

    private void Start()
    {
        fearLevel.localScale = new Vector3(0f, 1f, 1f);
    }

    public void SetPlayer(Transform playerReference)
    {
        player = playerReference;
        fearComponent = player.GetComponent<Fear>();
        fearComponent.fearEvent += OnFearIncrease;
    }

    private void OnFearIncrease(float currentFear, float maxFear)
    {
        text.text = $"Fear: {currentFear:0.00}/{maxFear:0}";
        float currentPercFear = currentFear / maxFear;
        fearLevel.localScale = new Vector3(currentPercFear, 1f, 1f);
    }
}
