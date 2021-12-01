using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform fearUI;
    [SerializeField] private Transform healthUI;
    public GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        fearUI.GetComponent<FearUI>().SetPlayer(player.transform);
        // healthUI.GetComponent<HealthUI>().SetPlayer(player.transform);
    }
}
