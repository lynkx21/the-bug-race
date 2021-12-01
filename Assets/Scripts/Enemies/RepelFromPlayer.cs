using Assets.Scripts.Player;
using UnityEngine;

public class RepelFromPlayer : MonoBehaviour {
    public float force = 20;
    public float radiusFromPlayer = 2.0f;
    
    private Rigidbody2D _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(ThePlayer.Instance.transform.position, radiusFromPlayer);
    }
    
    private void FixedUpdate() {
        var deltaPos = (transform.position - ThePlayer.Instance.transform.position);
        var distance = deltaPos.magnitude;
        if (distance < radiusFromPlayer) {
            var direction = deltaPos.normalized;
            _rb.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }
}