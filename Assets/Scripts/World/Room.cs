using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jam.Areas
{
    public enum WallOrientation
    {
        Left,
        Right,
        Top,
        Bottom,
    }

    public class Room : MonoBehaviour
    {
        public const int ROOM_SIDES = 4;
        [SerializeField] private Color background = Color.black;
        [SerializeField] private List<GameObject> walls;
        [SerializeField] private List<GameObject> doors;

        public Vector2 roomCoordinates;
        private bool areDoorsOpen = false;
        private bool isRoomClear = false; // false until player hasn't defeated all enemies
        // public event Action onRoomClearEvent;

        public void Awake()
        {
            walls = new List<GameObject>();
            doors = new List<GameObject>();
            isRoomClear = false;
            World.Instance.roomClearEvent += OnRoomClear;
        }

        private void Update()
        {
            if (isRoomClear && !areDoorsOpen)
            {
                OpenDoors();
            }
        }

        public void SetCoordinates(Vector2 coordinates)
        {
            roomCoordinates = coordinates;
        }

        public void CreateRoom(List<WallOrientation> exits, bool needToStepOut)
        {
            // TODO: change to sprite/texture
            Camera.main.backgroundColor = background;

            GameObject ground = new GameObject("Ground");
            Sprite groundSprite = Resources.Load<Sprite>("Sprites/ground_v2");
            ground.transform.position = Vector3.zero;
            AddSprite(ground, groundSprite, 0);
            ground.transform.parent = this.transform;


            for (int i = 0; i < ROOM_SIDES; i++)
            {
                // NOTE: do we need to store them?
                walls.Add(CreateWall((WallOrientation)i));
                if (exits.Contains((WallOrientation)i))
                {
                    doors.Add(CreateDoor((WallOrientation)i, needToStepOut));
                }
            }
        }

        private GameObject CreateWall(WallOrientation orientation)
        {
            GameObject wall;
            Sprite wallSprite;
            switch (orientation)
            {
                case WallOrientation.Left:
                    wall = new GameObject("Wall Left");
                    wallSprite = Resources.Load<Sprite>("Sprites/room_left");
                    wall.transform.position = new Vector3(-7.2f, 0f, 0f);
                    break;
                case WallOrientation.Right:
                    wall = new GameObject("Wall Right");
                    wallSprite = Resources.Load<Sprite>("Sprites/room_right");
                    wall.transform.position = new Vector3(7.2f, 0f, 0f);
                    break;
                case WallOrientation.Top:
                    wall = new GameObject("Wall Top");
                    wallSprite = Resources.Load<Sprite>("Sprites/room_top");
                    wall.transform.position = new Vector3(0f, 4.2f, 0f);
                    break;
                case WallOrientation.Bottom:
                    wall = new GameObject("Wall Bottom");
                    wallSprite = Resources.Load<Sprite>("Sprites/room_bottom");
                    wall.transform.position = new Vector3(0f, -4.2f, 0f);
                    break;
                default:
                    wall = null;
                    wallSprite = null;
                    break;
            }
            AddSprite(wall, wallSprite, 1);
            AddRigidBody(wall);
            AddWallCollider(wall, orientation);
            wall.transform.parent = this.transform;
            wall.tag = "Wall";
            
            return wall;
        }

        private GameObject CreateDoor(WallOrientation orientation, bool needToStepOut)
        {
            GameObject door;
            Sprite doorSprite;
            switch(orientation)
            {
                case WallOrientation.Left:
                    door = new GameObject("Door Left");
                    doorSprite = Resources.Load<Sprite>("Sprites/door_close_left_v2");
                    door.transform.position = new Vector3(-7.15f, 0f, 0f);
                    break;
                case WallOrientation.Right:
                    door = new GameObject("Door Right");
                    doorSprite = Resources.Load<Sprite>("Sprites/door_close_right_v2");
                    door.transform.position = new Vector3(7.15f, 0f, 0f);
                    break;
                case WallOrientation.Top:
                    door = new GameObject("Door Top");
                    // doorSprite = Resources.Load<Sprite>("Sprites/door_close_top");
                    doorSprite = Resources.Load<Sprite>("Sprites/door_close_top_v2");
                    door.transform.position = new Vector3(0f, 4.15f, 0f);
                    break;
                case WallOrientation.Bottom:
                    door = new GameObject("Door Bottom");
                    doorSprite = Resources.Load<Sprite>("Sprites/door_close_bottom_v2");
                    door.transform.position = new Vector3(0f, -4.15f, 0f);
                    break;
                default:
                    door = null;
                    doorSprite = null;
                    break;
            }
            AddSprite(door, doorSprite, 2);
            AddRigidBody(door);
            door.transform.parent = this.transform;
            door.AddComponent<Door>();
            Door doorComponent = door.GetComponent<Door>();
            doorComponent.orientation = orientation;
            doorComponent.needToStepOut = needToStepOut;

            return door;
        }

        private void OnRoomClear()
        {
            isRoomClear = true;
        }

        private void OpenDoors()
        {
            foreach (GameObject door in doors) {
                Door doorComponent = door.GetComponent<Door>();
                Sprite doorSprite;
                switch (doorComponent.orientation)
                {
                    case WallOrientation.Left:
                        doorSprite = Resources.Load<Sprite>("Sprites/door_open_left_v2");
                    break;
                    case WallOrientation.Right:
                        doorSprite = Resources.Load<Sprite>("Sprites/door_open_right_v2");
                        break;
                    case WallOrientation.Top:
                        doorSprite = Resources.Load<Sprite>("Sprites/door_open_top_v2");
                        break;
                    case WallOrientation.Bottom:
                        doorSprite = Resources.Load<Sprite>("Sprites/door_open_bottom_v2");
                        break;
                    default:
                        doorSprite = null;
                        break;
                }

                door.GetComponent<SpriteRenderer>().sprite = doorSprite;
                AddDoorTrigger(door, doorComponent.orientation);
                doorComponent.needToStepOut = false;
                /*
                if (roomCoordinates != Vector2.zero)
                {
                    doorComponent.needToStepOut = false;
                }
                else
                {
                    // doorComponent.needToStepOut = true;
                    doorComponent.stepOutEvent += OnDoorTriggerExit;
                }
                Debug.Log($"[Room] Need to step out? {doorComponent.needToStepOut}");
                */
            }

            areDoorsOpen = true;
        }

        private void OnDoorTriggerExit()
        {
            foreach (GameObject door in doors)
            {
                door.GetComponent<Door>().needToStepOut = false;
            }
        }

        private void AddSprite(GameObject obj, Sprite sprite, int order)
        {
            obj.AddComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingLayerName = "Room";
            spriteRenderer.sortingOrder = order;
        }

        private void AddDoorTrigger(GameObject obj, WallOrientation orientation)
        {
            obj.AddComponent<BoxCollider2D>();
            BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
            switch (orientation)
            {
                case WallOrientation.Left:
                    collider.offset = new Vector2(0.35f, 0f);
                    collider.size = new Vector2(0.6f, 0.8f);
                    break;
                case WallOrientation.Right:
                    collider.offset = new Vector2(-0.35f, 0f);
                    collider.size = new Vector2(0.6f, 0.8f);
                    break;
                case WallOrientation.Top:
                    collider.offset = new Vector2(0f, -0.35f);
                    collider.size = new Vector2(0.8f, 0.6f);
                    break;
                case WallOrientation.Bottom:
                    collider.offset = new Vector2(0f, 0.35f);
                    collider.size = new Vector2(0.8f, 0.6f);
                    break;
                default:
                    break;
            }
        }

        private void AddWallCollider(GameObject obj, WallOrientation orientation)
        {
            obj.AddComponent<BoxCollider2D>();
            BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
            switch (orientation)
            {
                case WallOrientation.Left:
                    collider.offset = new Vector2(-0.05f, 0f);
                    collider.size = new Vector2(1.5f, 10f);
                    break;
                case WallOrientation.Right:
                    collider.offset = new Vector2(0.05f, 0f);
                    collider.size = new Vector2(1.5f, 10f);
                    break;
                case WallOrientation.Top:
                    collider.offset = new Vector2(0f, 0.05f);
                    collider.size = new Vector2(16f, 1.5f);
                    break;
                case WallOrientation.Bottom:
                    collider.offset = new Vector2(0f, -0.05f);
                    collider.size = new Vector2(16f, 1.5f);
                    break;
                default:
                    break;
            }
        }

        private void AddRigidBody(GameObject obj)
        {
            obj.AddComponent<Rigidbody2D>();
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
        }
    }
}