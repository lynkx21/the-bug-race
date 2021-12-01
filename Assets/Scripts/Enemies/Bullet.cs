using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float rotationSpeed = 2.5f;
    public float bulletSpeed = 6f;
    public float lifeTime = 3f;
    public bool shouldScale;
    [Range(0, 3)] public float initialScale = 1f;
    [Range(0, 3)] public float finalScale = 1f;
    public float scaleTime = 1f;
    
    protected Vector3 _initialObjectScale;
    protected Vector3 _finalObjectScale;
    protected float _elapsedTime;
    
    public Vector3 Direction { get; set; }

    protected void Start() {
        if (shouldScale) {
            SetInitialScale();
        }
    }

    protected virtual void Update()
    {
        MoveBullet();

        if (shouldScale)
        {
            UpdateScale();
        }

        UpdateLifeTime();
    }

    protected virtual void UpdateLifeTime()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void SetInitialScale()
    {
        _initialObjectScale = transform.localScale * initialScale;
        _finalObjectScale = transform.localScale * finalScale;
        transform.localScale = _initialObjectScale;
    }

    protected virtual void UpdateScale()
    {
        _elapsedTime += Time.deltaTime;
        transform.localScale = Vector3.Lerp(_initialObjectScale, _finalObjectScale, _elapsedTime / scaleTime);
    }

    protected virtual void MoveBullet()
    {
        transform.Rotate(Vector3.forward * rotationSpeed);
        transform.position += Direction * bulletSpeed * Time.deltaTime;
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        var thisCollider = GetComponent<Collider2D>();
        if (thisCollider.CompareTag("Spiderweb") && other.CompareTag("Player")) {
            bulletSpeed = 0;
            rotationSpeed = 0;
        }
        if (thisCollider.CompareTag("Projectile") && other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}