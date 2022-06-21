using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int CurHP;
    public int MaxHP;
    public int Coins;
    public bool HasKey;
    public SpriteRenderer Sr;

    public LayerMask MoveLayerMask;


    private void Move(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, MoveLayerMask);

        if(hit.collider == null)
        {
            transform.position += new Vector3(dir.x, dir.y, 0f);
            EnemyManager.Instance.OnPlayerMove();
        }
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.up);
        }
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.down);
        }
    }
    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.left);
        }
    }
    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.right);
        }
    }

    public void OnAttackUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            TryAttack(Vector2.up);
    }

    public void OnAttackDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            TryAttack(Vector2.down);
    }
    public void OnAttackLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            TryAttack(Vector2.left);
    }
    public void OnAttackRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            TryAttack(Vector2.right);
    }

    private void TryAttack(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, 1 << 9);

        if (hit.collider != null)
            hit.transform.GetComponent<Enemy>().TakeDamage(1);
    }

    public void TakeDamage(int damageToTake)
    {
        CurHP -= damageToTake;

        StartCoroutine(DamageFlash());

        if (CurHP <= 0)
            SceneManager.LoadScene(0);

    }

    IEnumerator DamageFlash()
    {
        Color defaultColor = Sr.color;
        Sr.color = Color.white;

        yield return new WaitForSeconds(0.05f);

        Sr.color = defaultColor;
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    public bool Addhealth(int amount)
    {
        if (CurHP + amount <= MaxHP)
        {
            CurHP += amount;
            return true;
        }
        return false;
    }
}
