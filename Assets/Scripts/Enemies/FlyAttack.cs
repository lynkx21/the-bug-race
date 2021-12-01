using System;
using System.Collections;
using Assets.Scripts.Enemies;
using Assets.Scripts.Player;
using UnityEngine;

public class FlyAttack : MonoBehaviour {
    public BoxCollider2D projectileTrigger;
    public float reloadTime = 5.0f;
    public float force = 10f;
    public float attackDuration = 3f;
    private Rigidbody2D _rb;
    private float _currentAttackTime = 0;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        projectileTrigger.enabled = false;
        reloadTime = Mathf.Max(reloadTime, attackDuration);
        attackDuration = Mathf.Min(attackDuration, reloadTime);
    }

    private void FixedUpdate() {
        _currentAttackTime += Time.deltaTime;
        if (_currentAttackTime > reloadTime) {
            StartCoroutine(nameof(Attack));
            _currentAttackTime = 0.0f;
        }
    }

    private IEnumerator Attack() {
        float currentAttackTime = 0;

        projectileTrigger.enabled = true;
        
        // Disable any other script during the attack
        var components = GetComponents<MonoBehaviour>();
        foreach (var component in components) {
            if (component.name == nameof(FlyAttack) || component.name == nameof(Enemy))
                continue;
            component.enabled = false;
        }
        
        // Fly towards the player with the given force
        var direction = (ThePlayer.Instance.transform.position - _rb.transform.position).normalized;
        _rb.AddForce(direction * force, ForceMode2D.Impulse);

        while (currentAttackTime < attackDuration) {
            currentAttackTime += Time.deltaTime;
            // Update the projectileTrigger position in respect of the enemy sprite
            projectileTrigger.transform.position = _rb.transform.position;
            projectileTrigger.transform.rotation = _rb.transform.rotation;
            yield return null;
        }
        
        // Re-enable all scripts
        foreach (var component in components) {
            component.enabled = true;
        }
        
        // Turn off projectTrigger since we don't need since a new attack
        projectileTrigger.enabled = false;
    }
}