using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player Player;
    public int Health;
    public int Damage;
    public float AttackChance = 0.5f;

    public GameObject DeathDropPrefab;
    public SpriteRenderer Sr;
    public LayerMask MoveLayerMask;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
    }

    public void TakeDamage(int damageToTake)
    {
        Health -= damageToTake;
        if (Health <= 0)
        {
            if (DeathDropPrefab != null)
                Instantiate(DeathDropPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        StartCoroutine(DamageFlash());

        if (Random.value > AttackChance)
            Player.TakeDamage(Damage);
    }

    IEnumerator DamageFlash()
    {
        Color defaultColor = Sr.color;
        Sr.color = Color.white;

        yield return new WaitForSeconds(0.05f);

        Sr.color = defaultColor;
    }

    public void Move()
    {
        if (Random.value < 0.5f)
            return;

        Vector3 dir = Vector3.zero;
        bool canMove = false;

        while (canMove == false)
        {
            dir = GetRandomDirection();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, MoveLayerMask);
            if (hit.collider == null)
                canMove = true;
        }

        transform.position += dir;
    }

    private Vector3 GetRandomDirection()
    {
        int ran = Random.Range(0, 4);

        if (ran == 0)
            return Vector3.up;
        else if (ran == 1)
            return Vector3.down;
        else if (ran == 2)
            return Vector3.left;
        else if (ran == 3)
            return Vector3.right;

        return Vector3.zero;
    }
}
