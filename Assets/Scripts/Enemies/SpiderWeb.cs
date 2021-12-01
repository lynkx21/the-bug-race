using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : Bullet
{
    public bool shouldGetTransparent;
    private SpriteRenderer _spriteRenderer;
    private Color _initialSpriteColor;
    private Color _finalSpriteColor;
    private float _elapsedTimeColor = 0f;

    private float halfLifeTime;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialSpriteColor = _spriteRenderer.color;
        _finalSpriteColor = new Color(
            _initialSpriteColor.r, _initialSpriteColor.g, _initialSpriteColor.b, 0f);

        halfLifeTime = lifeTime / 2f;
    }

    protected override void Update()
    {
        base.MoveBullet();

        if (shouldScale)
        {
            UpdateScale();
        }

        if (shouldGetTransparent)
        {
            UpdateTransparency();
        }

        UpdateLifeTime();

        if (_elapsedTime >= halfLifeTime)
        {
            Destroy(GetComponent<Collider2D>());
        }
    }

    private void UpdateTransparency()
    {
        _elapsedTimeColor += Time.deltaTime;
        _spriteRenderer.color = Color.Lerp(_initialSpriteColor, _finalSpriteColor, (_elapsedTimeColor - halfLifeTime) / lifeTime);
    }

    private void StopMovement()
    {
        bulletSpeed = 0f;
        rotationSpeed = 0f;
    }

    public float GetLifeTime()
    {
        return halfLifeTime - _elapsedTime;
    }

    protected new void OnTriggerEnter2D(Collider2D other) {
        var thisCollider = GetComponent<Collider2D>();
        if (thisCollider.CompareTag("Spiderweb") && other.CompareTag("Player")) {
            StopMovement();
        }
        if (other.CompareTag("Wall"))
        {
            StopMovement();
        }
    }

    
}