using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif

namespace Assets.Scripts.Enemies {
    public class AggroArea : MonoBehaviour {
        public float radius = 120f;
        public float length = 3f;

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void OnDrawGizmos() {
            var direction = transform.up;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            float minAngle = (angle - radius / 2f + 90f) * Mathf.Deg2Rad;
            float maxAngle = (angle + radius / 2f + 90f) * Mathf.Deg2Rad;
            var minDirection = new Vector3(Mathf.Cos(minAngle), Mathf.Sin(minAngle), 0f);
            var maxDirection = new Vector3(Mathf.Cos(maxAngle), Mathf.Sin(maxAngle), 0f);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + direction * length);
            Gizmos.DrawLine(transform.position, transform.position + minDirection * length);
            Gizmos.DrawLine(transform.position, transform.position + maxDirection * length);

//#if UNITY_EDITOR
//            Handles.color = Color.cyan;
//            Handles.DrawWireArc(transform.position, transform.forward, minDirection, radius, length);
//#endif
        }
    }
}