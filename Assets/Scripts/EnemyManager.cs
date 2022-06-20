using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public List<Enemy> Enemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void OnPlayerMove()
    {
        StartCoroutine(MoveEnemies());
    }

    IEnumerator MoveEnemies()
    {
        yield return new WaitForFixedUpdate();

        foreach (Enemy enemy in Enemies)
        {
            if (enemy != null)
                enemy.Move();
        }
    }
}
