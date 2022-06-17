using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Door Objects")]
    public Transform NorthDoor;
    public Transform SouthDoor;
    public Transform WestDoor;
    public Transform EastDoor;

    [Header("Wall Objects")]
    public Transform NorthWall;
    public Transform SouthWall;
    public Transform WestWall;
    public Transform EasthWall;

    [Header("Values")]
    public int InsideWidth;
    public int InsideHeight;

    [Header("Prefabs")]
    public GameObject EnemyPrefab;
    public GameObject CoinPrefab;
    public GameObject HealthPrefab;
    public GameObject KeyPrefab;
    public GameObject ExitDoorPrefab;

    private List<Vector3> _usedPositions = new List<Vector3>();

    public void GenerateInterior()
    {

    }

    public void SpawnPrefab(GameObject prefab, int min=0, int max = 0)
    {

    }
}
