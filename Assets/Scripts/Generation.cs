using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public static Generation Instance;
    
    public int MapWidth = 7;
    public int MapHeihgt = 7;
    public int RoomsToGenerate = 12;

    private int _roomCount;
    private bool _roomsInstantiated;

    private Vector2 _firstRoomPos;

    private bool[,] _map;
    public GameObject RoomPrefab;

    private List<Room> _roomObjects = new List<Room>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Random.InitState(74742584);
        Generate();
    }

    public void Generate()
    {
        _map = new bool[MapWidth, MapHeihgt];
        CheckRoom(3, 3, 0, Vector2.zero, true);
        InstantiateRooms();
        FindObjectOfType<Player>().transform.position = _firstRoomPos * 12;
    }

    private void CheckRoom(int x,int y, int remaning, Vector2 generalDirection,bool firstRoom = false)
    {
        if (_roomCount >= RoomsToGenerate)
            return;
        if (x < 0 || x > MapWidth - 1 || y < 0 || y > MapHeihgt - 1)
            return;
        if (firstRoom == false && remaning <= 0)        
            return;
        if (_map[x, y] == true) 
            return;

        if (firstRoom == true)
            _firstRoomPos = new Vector2(x, y);

        _roomCount++;
        _map[x, y] = true;

        bool north = Random.value > (generalDirection == Vector2.up ? 0.2f : 0.8f);
        bool south = Random.value > (generalDirection == Vector2.down ? 0.2f : 0.8f);
        bool east = Random.value > (generalDirection == Vector2.right ? 0.2f : 0.8f);
        bool west = Random.value > (generalDirection == Vector2.left ? 0.2f : 0.8f);

        int maxRemaning = RoomsToGenerate / 4;

        if(north || firstRoom)
        {
            CheckRoom(x, y + 1, firstRoom ? maxRemaning : remaning - 1, firstRoom ? Vector2.up : generalDirection);
        }
        if (south || firstRoom)
        {
            CheckRoom(x, y - 1, firstRoom ? maxRemaning : remaning - 1, firstRoom ? Vector2.down : generalDirection);
        }
        if (east || firstRoom)
        {
            CheckRoom(x + 1, y , firstRoom ? maxRemaning : remaning - 1, firstRoom ? Vector2.right : generalDirection);
        }
        if (west || firstRoom)
        {
            CheckRoom(x - 1, y , firstRoom ? maxRemaning : remaning - 1, firstRoom ? Vector2.left : generalDirection);
        }
    }

    private void InstantiateRooms()
    {

    }

    private void CalculateKeyAndExit()
    {

    }
}
