using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float heal; // 20%

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float recovery = GameManager.instance.player.playerMaxHp * (heal / 100);

            if ((GameManager.instance.player.playerMaxHp - GameManager.instance.player.playerHp) <= recovery)
            {
                GameManager.instance.player.playerHp += (GameManager.instance.player.playerMaxHp - GameManager.instance.player.playerHp);
            }
            else
            {
                GameManager.instance.player.playerHp += recovery;
            }
            Destroy(gameObject);
        }
    }
}
