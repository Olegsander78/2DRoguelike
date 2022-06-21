using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Coin,
    Health
}

public class Pickup : MonoBehaviour
{
    public PickupType Type;
    public int Value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Type == PickupType.Coin)
            {
                collision.GetComponent<Player>().AddCoins(Value);
                Destroy(gameObject);
            }else if (Type == PickupType.Health)
            {
                if (collision.GetComponent<Player>().Addhealth(Value))
                    Destroy(gameObject);
            }
        }
    }

}
