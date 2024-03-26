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
            float recovery = GameManager.Instance.player.playerMaxHp * (heal / 100);

            if ((GameManager.Instance.player.playerMaxHp - GameManager.Instance.player.playerHp) <= recovery)
            {
                GameManager.Instance.player.playerHp += (GameManager.Instance.player.playerMaxHp - GameManager.Instance.player.playerHp);
            }
            else
            {
                GameManager.Instance.player.playerHp += recovery;
            }
            Destroy(gameObject);
        }
    }
}
