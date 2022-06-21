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
        if(Random.value < Generation.Instance.EnemySpawnChance)
            SpawnPrefab(EnemyPrefab, 1, Generation.Instance.MaxEnemiesPerRoom + 1);

        if (Random.value < Generation.Instance.CoinSpawnChance)
            SpawnPrefab(CoinPrefab, 1, Generation.Instance.MaxCoinsPerRoom + 1);

        if (Random.value < Generation.Instance.HealthSpawnChance)
            SpawnPrefab(HealthPrefab, 1, Generation.Instance.MaxHealthPerRoom + 1);
    }

    public void SpawnPrefab(GameObject prefab, int min=0, int max = 0)
    {
        int num = 1;

        if (min != 0 || max != 0)
            num = Random.Range(min, max);

        for (int x = 0; x < num; ++x)
        {
            GameObject obj = Instantiate(prefab);
            Vector3 pos = transform.position + 
                new Vector3(Random.Range(-InsideWidth / 2, (InsideWidth / 2) + 1), Random.Range(-InsideHeight / 2, (InsideHeight / 2) + 1), 0f);

            while (_usedPositions.Contains(pos))
            {
                pos = transform.position + new Vector3(Random.Range(-InsideWidth / 2, (InsideWidth / 2) + 1), Random.Range(-InsideHeight / 2, (InsideHeight / 2) + 1), 0f);
            }

            obj.transform.position = pos;
            _usedPositions.Add(pos);

            if (prefab == EnemyPrefab)
                EnemyManager.Instance.Enemies.Add(obj.GetComponent<Enemy>());
        }

    }
}
