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
            if (File.Exists(GameManager.instance.path + $"{i}"))
            {
                savefile[i] = true;

                GameManager.instance.nowSlot = i;
                GameManager.instance.LoadPlayerData();
                slotText[i].text = GameManager.instance.nowPlayer.playerName;
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }

        GameManager.instance.DataClear();
    }

    public void Slot(int number)
    {
        GameManager.instance.nowSlot = number;

        if (savefile[number])
        {
            GameManager.instance.LoadPlayerData();
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
        if (!savefile[GameManager.instance.nowSlot]) // 초기스탯
        {
            GameManager.instance.nowPlayer.playerName = newPlayerName.text;
            GameManager.instance.nowPlayer.playerMaxHp = 100;
            GameManager.instance.nowPlayer.playerSpeed = 3;
            GameManager.instance.nowPlayer.lootingRange = 0.5f;
            GameManager.instance.nowPlayer.attackDmg = 3;
            GameManager.instance.nowPlayer.attackSpeed = 1;
            GameManager.instance.nowPlayer.attackRange = 5;
            GameManager.instance.nowPlayer.bulletSpeed = 5;
            GameManager.instance.nowPlayer.bulletLifeTime = 2;
            GameManager.instance.nowPlayer.bulletPen = 0;
            GameManager.instance.nowPlayer.expMultipler = 1;
            GameManager.instance.nowPlayer.coin = 100;

            GameManager.instance.SavePlayerData();
        }
        GameManager.instance.LoadItemBase();
        SceneManager.LoadScene(1);
    }
}
