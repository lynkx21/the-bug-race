using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartBullet : Bullet 
{
    public bool shouldGetTransparent;
    private SpriteRenderer _spriteRenderer;
    private Color _initialSpriteColor;
    private Color _finalSpriteColor;
    private float _elapsedTimeColor = 0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialSpriteColor = _spriteRenderer.color;
        _finalSpriteColor = new Color(
            _initialSpriteColor.r, _initialSpriteColor.g, _initialSpriteColor.b, 0f);
    }

    protected override void Update()
    {
        if (shouldScale)
        {
            UpdateScale();
        }

        if (shouldGetTransparent)
        {
            UpdateTransparency();
        }

        UpdateLifeTime();
    }

    private void UpdateTransparency()
    {
        _elapsedTimeColor += Time.deltaTime;
        _spriteRenderer.color = Color.Lerp(_initialSpriteColor, _finalSpriteColor, _elapsedTimeColor / scaleTime);
    }

    protected new void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    /*
    public float rotationSpeed = 0f;
    public float bulletSpeed = 0f;
    public float lifeTime = 5f;
    public bool shouldScale;
    [Range(0, 3)] public float initialScale = 0.5f;
    [Range(0, 3)] public float finalScale = 3f;
    public float scaleTime = 5f;

    private SpriteRenderer _spriteRenderer;
    private Color _initialSpriteColor;
    private Color _finalSpriteColor;
    private Vector3 _initialObjectScale;
    private Vector3 _finalObjectScale;
    private float _elapsedTime;
    
    public Vector3 Direction { get; set; }


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialSpriteColor = _spriteRenderer.color;
        _finalSpriteColor = new Color(
            _initialSpriteColor.r, _initialSpriteColor.g, _initialSpriteColor.b, 0f);
    }

    private void Start() {
        if (shouldScale) {
            _initialObjectScale = transform.localScale * initialScale;
            _finalObjectScale = transform.localScale * finalScale;
            transform.localScale = _initialObjectScale;
        }
    }

    private void Update() {
        // transform.Rotate(Vector3.forward * rotationSpeed);
        // transform.position += Direction * bulletSpeed * Time.deltaTime;

        if (shouldScale) {
            _elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(_initialObjectScale, _finalObjectScale, _elapsedTime / scaleTime);
            _spriteRenderer.color = Color.Lerp(_initialSpriteColor, _finalSpriteColor, _elapsedTime / scaleTime);
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        //Debug.Log("Collision!");
        //var thisCollider = GetComponent<Collider2D>();
        //if (thisCollider.CompareTag("Spiderweb") && other.CompareTag("Player"))
        //{
        //    bulletSpeed = 0;
        //    rotationSpeed = 0;
        //}
        //if (thisCollider.CompareTag("Projectile") && other.CompareTag("Enemy"))
        //{
        //    Destroy(gameObject);
        //}

    }
    */
}