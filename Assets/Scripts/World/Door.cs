using System;
using UnityEngine;

namespace Jam.Areas
{
    public class Door : MonoBehaviour
    {
        public WallOrientation orientation { get; set; }
        public event Action<Vector3> exitRoomEvent;
        public event Action stepOutEvent;
        public bool needToStepOut = true;

        private void Awake()
        {
            exitRoomEvent += World.Instance.OnExitRoom;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            // Debug.Log($"[Door] Need to step out? {needToStepOut}");
            if (collision.gameObject.tag == "Player" && !needToStepOut)
            {
                if (exitRoomEvent != null)
                {
                    exitRoomEvent?.Invoke(transform.position);
                }
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                // Debug.Log("[Door] Freeing colliders");
                stepOutEvent?.Invoke();
            }
        }
    }
}