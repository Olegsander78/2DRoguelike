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

    public float EnemySpawnChance;
    public float CoinSpawnChance;
    public float HealthSpawnChance;

    public int MaxEnemiesPerRoom;
    public int MaxCoinsPerRoom;
    public int MaxHealthPerRoom;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Random.InitState(765);
        Generate();
    }

    public void OnPlayerMove()
    {
        Vector2 playerPos = FindObjectOfType<Player>().transform.position;
        Vector2 roomPos = new Vector2(((int)playerPos.x + 6) / 12, ((int)playerPos.y + 6) / 12);

        UI.Instance.Map.texture = MapTextureGenerator.Generate(_map, roomPos);

    }

    public void Generate()
    {
        _map = new bool[MapWidth, MapHeihgt];
        CheckRoom(3, 3, 0, Vector2.zero, true);
        InstantiateRooms();
        FindObjectOfType<Player>().transform.position = _firstRoomPos * 12;

        UI.Instance.Map.texture = MapTextureGenerator.Generate( _map, _firstRoomPos);
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
        if (_roomsInstantiated)
            return;

        _roomsInstantiated = true;

        for (int x = 0; x < MapWidth; ++x)
        {
            for (int y = 0; y < MapHeihgt; ++y)
            {
                if (_map[x, y] == false)
                    continue;

                GameObject roomObj = Instantiate(RoomPrefab, new Vector3(x, y, 0f) * 12, Quaternion.identity);
                Room room = roomObj.GetComponent<Room>();

                if (y < MapHeihgt - 1 && _map[x, y + 1] == true)
                {
                    room.NorthDoor.gameObject.SetActive(true);
                    room.NorthWall.gameObject.SetActive(false);
                }

                if (y > 0 && _map[x, y - 1] == true)
                {
                    room.SouthDoor.gameObject.SetActive(true);
                    room.SouthWall.gameObject.SetActive(false);
                }

                if (x < MapWidth - 1 && _map[x + 1, y] == true)
                {
                    room.EastDoor.gameObject.SetActive(true);
                    room.EasthWall.gameObject.SetActive(false);
                }

                if (x > 0 && _map[x-1, y] == true)
                {
                    room.WestDoor.gameObject.SetActive(true);
                    room.WestWall.gameObject.SetActive(false);
                }

                if(_firstRoomPos != new Vector2(x, y))                
                    room.GenerateInterior();

                _roomObjects.Add(room);
            }

        }

        CalculateKeyAndExit();
    }

    private void CalculateKeyAndExit()
    {
        float maxDist = 0;
        Room a = null;
        Room b = null;

        foreach (Room aRoom in _roomObjects)
        {
            foreach (Room bRoom in _roomObjects)
            {
                float dist = Vector3.Distance(aRoom.transform.position, bRoom.transform.position);

                if (dist > maxDist)
                {
                    a = aRoom;
                    b = bRoom;
                    maxDist = dist;
                }
            }

        }

        a.SpawnPrefab(a.KeyPrefab);
        b.SpawnPrefab(b.ExitDoorPrefab);
    }
}
