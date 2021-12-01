using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 0.1f;
    public Vector3 currentDirection;
    public Vector3 facingDirection = Vector3.up;
    private float nextChangeDirectionTime;
    private float timer;
    private float maxHorizontalLimit = 4f; // 5.5f;
    private float maxVerticalLimit = 2f; // 3.5f;

    private void Start()
    {
        nextChangeDirectionTime = Random.Range(.5f, 2f);
        timer = nextChangeDirectionTime;
        GenerateRandomDirection();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GenerateRandomDirection();
            nextChangeDirectionTime = Random.Range(.5f, 2f);
            timer = nextChangeDirectionTime;
        }

        // Check boundries!
        Vector3 newPosition = transform.position + Time.deltaTime * speed * currentDirection.normalized;
        if (Mathf.Abs(newPosition.x) <= maxHorizontalLimit && Mathf.Abs(newPosition.y) <= maxVerticalLimit)
        {
            transform.position = newPosition;
        }
    }

    private void GenerateRandomDirection()
    {
        float horizontalMovement = Random.Range(-1, 2);
        float verticalMovement = Random.Range(-1, 2);
        Vector3 newDirection = Vector3.zero;
        if (horizontalMovement != 0)
        {
            newDirection += Vector3.right * horizontalMovement;
        }

        if (verticalMovement != 0)
        {
            newDirection += Vector3.up * verticalMovement;
        }

        currentDirection = newDirection;

        if (currentDirection != Vector3.zero)
        {
            facingDirection = currentDirection;
            float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg - 90f;
            var desiredRotation = Quaternion.Euler(Vector3.forward * angle);
            transform.rotation = desiredRotation;
            // transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation,
            //    (Time.time % Time.deltaTime) / Time.deltaTime * rotationSpeed);
        }
    }
}
