using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotSelect : MonoBehaviour
{
    public GameObject creat;
    public Text[] slotText;
    public Text newPlayerName;

    bool[] savefile = new bool[3];

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(GameManager.Instance.path + $"{i}"))
            {
                savefile[i] = true;

                GameManager.Instance.nowSlot = i;
                GameManager.Instance.LoadPlayerData();
                slotText[i].text = GameManager.Instance.nowPlayer.playerName;
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }

        GameManager.Instance.DataClear();
    }

    public void Slot(int number)
    {
        GameManager.Instance.nowSlot = number;

        if (savefile[number])
        {
            GameManager.Instance.LoadPlayerData();
            GoLobby();
        }
        else
        {
            Creat();
        }
    }

    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }

    public void GoLobby()
    {
        if (!savefile[GameManager.Instance.nowSlot])
        {
            GameManager.Instance.nowPlayer.playerName = newPlayerName.text;
            GameManager.Instance.nowPlayer.playerMaxHp = 100;
            GameManager.Instance.nowPlayer.playerSpeed = 3;
            GameManager.Instance.nowPlayer.lootingRange = 0.5f;
            GameManager.Instance.nowPlayer.attackDmg = 3;
            GameManager.Instance.nowPlayer.attackSpeed = 1;
            GameManager.Instance.nowPlayer.attackRange = 5;
            GameManager.Instance.nowPlayer.bulletSpeed = 5;
            GameManager.Instance.nowPlayer.bulletLifeTime = 2;
            GameManager.Instance.nowPlayer.bulletPen = 0;
            GameManager.Instance.nowPlayer.expMultipler = 1;
            GameManager.Instance.nowPlayer.coin = 100;

            GameManager.Instance.SavePlayerData();
            GameManager.Instance.LoadItemBase();
        }
        SceneManager.LoadScene(1);
    }
}
