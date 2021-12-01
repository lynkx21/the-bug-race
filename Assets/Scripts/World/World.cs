using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Areas
{
    public class World : MonoBehaviour
    {
        public static World Instance { get; private set; }

        private Dictionary<Vector2, List<WallOrientation>> worldMap;
        private const int MAX_MAP_EXTENSION = 10;
        [SerializeField] private GameObject currentRoom = null;
        [SerializeField] private static Vector2 worldCoordinates = Vector2.zero;
        public event Action<WallOrientation> exitRoomEvent;
        public event Action<Vector2> enterRoomEvent;
        public event Action roomClearEvent;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
            // DontDestroyOnLoad(this.gameObject);
            GenerateWorldMap();
            GenerateRoom(worldMap[worldCoordinates], false);
        }

        private void GenerateWorldMap()
        {
            worldMap = new Dictionary<Vector2, List<WallOrientation>>();

            List<Vector2> allCoordinates = new List<Vector2>();
            Vector2 startingCoordinates = Vector2.zero;
            Vector2 currentCoordinates = startingCoordinates;
            List<Vector2> nextCoordinates = new List<Vector2>();
            bool isMapComplete = false;

            for (int x = -MAX_MAP_EXTENSION; x <= MAX_MAP_EXTENSION; x++)
            {
                for (int y = -MAX_MAP_EXTENSION; y <= MAX_MAP_EXTENSION; y++)
                {
                    allCoordinates.Add(new Vector2(x, y));
                }
            }

            while (!isMapComplete)
            {
                List<WallOrientation> exits = new List<WallOrientation>();
                List<WallOrientation> availableExits = new List<WallOrientation>();
                for (int i = 0; i < Room.ROOM_SIDES; i++)
                {
                    WallOrientation orientation = (WallOrientation)i;
                    Vector2 incomingCoordinates = GetCloseCoordinates(currentCoordinates, orientation);
                    if (worldMap.ContainsKey(incomingCoordinates))
                    {
                        if (worldMap[incomingCoordinates].Contains(GetOppositeOrientation(orientation)))
                            exits.Add(orientation);
                    }
                    else if (UnityEngine.Random.Range(0f, 1f) > 0.4f)
                    {
                        Vector2 candidateCoordinates = GetCloseCoordinates(currentCoordinates, orientation);
                        if (Mathf.Abs(candidateCoordinates.x) <= MAX_MAP_EXTENSION && Mathf.Abs(candidateCoordinates.y) <= MAX_MAP_EXTENSION)
                        {
                            exits.Add(orientation);
                            if (!nextCoordinates.Contains(candidateCoordinates))
                            {
                                nextCoordinates.Add(candidateCoordinates);
                            }
                        }
                    }
                    else
                    {
                        availableExits.Add(orientation);
                    }
                }
                // Failsafe measure
                if (exits.Count == 0 && availableExits.Count > 0)
                {
                    WallOrientation exit = (WallOrientation)UnityEngine.Random.Range(0, availableExits.Count);
                    exits.Add(exit);
                }
                worldMap.Add(currentCoordinates, exits);
                bool isCoordinateRemoved = allCoordinates.Remove(currentCoordinates);
                
                if (allCoordinates.Count == 0)
                {
                    isMapComplete = true;
                }
                else
                {
                    if (nextCoordinates.Count > 0)
                    {
                        currentCoordinates = nextCoordinates[0];
                        nextCoordinates.RemoveAt(0);
                    }
                    else
                    {
                        currentCoordinates = allCoordinates[0];
                    }
                }
            }

        }

        private WallOrientation GetOppositeOrientation(WallOrientation orientation)
        {
            switch (orientation)
            {
                case WallOrientation.Left:
                    return WallOrientation.Right;
                case WallOrientation.Right:
                    return WallOrientation.Left;
                case WallOrientation.Top:
                    return WallOrientation.Bottom;
                case WallOrientation.Bottom:
                    return WallOrientation.Top;
                default:
                    return WallOrientation.Top;
            }
        }

        private Vector2 GetCloseCoordinates(Vector2 currentCoordinates, WallOrientation orientation)
        {
            Vector2 candidateCoordinates;
            switch (orientation)
            {
                case WallOrientation.Left:
                    candidateCoordinates = currentCoordinates + Vector2.left;
                    break;
                case WallOrientation.Right:
                    candidateCoordinates = currentCoordinates + Vector2.right;
                    break;
                case WallOrientation.Top:
                    candidateCoordinates = currentCoordinates + Vector2.up;
                    break;
                case WallOrientation.Bottom:
                    candidateCoordinates = currentCoordinates + Vector2.down;
                    break;
                default:
                    candidateCoordinates = currentCoordinates + Vector2.zero;
                    break;
            }
            return candidateCoordinates;
        }

        private void GenerateRoom(List<WallOrientation> exits, bool needToStepOut)
        {
            GameObject room = new GameObject("Room");
            room.AddComponent<Room>();
            Room roomComponent = room.GetComponent<Room>();
            roomComponent.Awake();
            roomComponent.SetCoordinates(worldCoordinates);
            roomComponent.CreateRoom(exits, needToStepOut);
            room.transform.parent = this.transform;
            currentRoom = room;
        }

        public void OnClearedRoom()
        {
            roomClearEvent?.Invoke();
        }

        public void OnExitRoom(Vector3 exitPoint)
        {
            float horizontalRoomChange = exitPoint.x == 0 ? 0 : Mathf.Sign(exitPoint.x);
            float verticalRoomChange = exitPoint.y == 0 ? 0 : Mathf.Sign(exitPoint.y);
            // Debug.Log($"Before: {worldCoordinates}");
            worldCoordinates += new Vector2(horizontalRoomChange, verticalRoomChange);
            // Debug.Log($"After: {worldCoordinates}");

            GameObject room = transform.GetChild(0).gameObject;
            Destroy(room);

            WallOrientation exitSide = GetExitSide(horizontalRoomChange, verticalRoomChange);
            if (exitRoomEvent != null)
            {
                exitRoomEvent.Invoke(exitSide);
            }

            GenerateRoom(worldMap[worldCoordinates], true);

            if (enterRoomEvent != null)
            {
                enterRoomEvent.Invoke(worldCoordinates);
            }
        }

        private WallOrientation GetExitSide(float horizontalRoomChange, float verticalRoomChange)
        {
            WallOrientation exitSide;
            if (horizontalRoomChange != 0)
            {
                if (horizontalRoomChange > 0)
                {
                    exitSide = WallOrientation.Right;
                }
                else
                {
                    exitSide = WallOrientation.Left;
                }
            }
            else
            {
                if (verticalRoomChange > 0)
                {
                    exitSide = WallOrientation.Top;
                }
                else
                {
                    exitSide = WallOrientation.Bottom;
                }
            }
            return exitSide;
        }
    }
}