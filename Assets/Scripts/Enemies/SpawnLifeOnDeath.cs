using UnityEngine;

namespace Assets.Scripts.Enemies {
    public class SpawnLifeOnDeath : MonoBehaviour {
        public GameObject lifePrefab;

        private void OnDestroy() {
            if(!gameObject.scene.isLoaded) return;
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        }
    }
}