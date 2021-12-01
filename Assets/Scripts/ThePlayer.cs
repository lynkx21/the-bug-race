using UnityEngine;

namespace Assets.Scripts.Player {
    public class ThePlayer : MonoBehaviour {
        private static ThePlayer _instance;
        private Movement moveComponent;
        private Fart fartComponent;
        private Shoot shootComponent;

        public static ThePlayer Instance {
            get {
                if (_instance == null)
                    _instance = FindObjectOfType<ThePlayer>();
                return _instance;
            }
        }

        private void Awake()
        {
            moveComponent = gameObject.GetComponent<Movement>();
            fartComponent = gameObject.GetComponent<Fart>();
            shootComponent = gameObject.GetComponent<Shoot>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Spiderweb")) {
                moveComponent.canMove = false;
                shootComponent.enabled = false;
                fartComponent.enabled = false;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            moveComponent.canMove = true;
            shootComponent.enabled = true;
            fartComponent.enabled = true;
        }
    }
}
