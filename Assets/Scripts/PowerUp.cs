using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private bool canMove;
    [SerializeField] private float speed;

    private void Update()
    {
        if (canMove)
        {
            transform.position += Vector3.up * 0.005f * Mathf.Sin(Time.time * speed);
        }
    }
}
