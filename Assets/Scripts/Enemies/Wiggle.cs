using UnityEngine;

namespace Assets.Scripts.Enemies {
    public class Wiggle : MonoBehaviour {
        public float speed = 1;
        public float amplitude = 2;
        public int octaves = 4;
        private Vector3 destination;
        private int currentTime;
        
        private void FixedUpdate() {
            // if number of frames played since last change of direction > octaves create a new destination
            if (currentTime > octaves) {
                currentTime = 0;
                destination = generateRandomVector(amplitude);
            }

            // smoothly moves the object to the random destination
            var currentVelocity = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref currentVelocity, speed);
            currentTime++;
        }
        
        private static Vector3 generateRandomVector(float amp) {
            var result = Vector3.zero;
            result.x = Random.Range(-amp, amp);
            result.y = Random.Range(-amp, amp);
            return result;
        }
        
    }
}