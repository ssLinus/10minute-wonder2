using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float heal; // 20

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if ((GameManager.instance.playerMaxHp - GameManager.instance.player.playerHp) <= heal)
            {
                GameManager.instance.player.playerHp += (GameManager.instance.playerMaxHp - GameManager.instance.player.playerHp);
            }
            else
            {
                GameManager.instance.player.playerHp += heal;
            }
            Destroy(gameObject);
        }
    }
}
