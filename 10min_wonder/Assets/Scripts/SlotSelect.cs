using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotSelect : MonoBehaviour
{
    public GameObject creat;
    public Text[] slotText;
    public Text newPlayerName;
    public GameObject[] saveDeletes;
    public GameObject saveDeleteUi;
    public Button deleteBtn;

    bool[] savefile = new bool[3];

    void Start()
    {
        SlotUpdate();
    }

    public void SlotUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(GameManager.instance.path + $"{i}"))
            {
                savefile[i] = true;

                GameManager.instance.nowSlot = i;
                GameManager.instance.LoadPlayerData();
                slotText[i].text = GameManager.instance.nowPlayer.playerName + "\n"
                    + "High Score " + GameManager.instance.highScore.ToString("000000");
                saveDeletes[i].SetActive(true);
            }
            else
            {
                slotText[i].text = "비어있음";
                saveDeletes[i].SetActive(false);
            }
        }
        GameManager.instance.DataClear();
    }

    public void Slot(int number)
    {
        GameManager.instance.nowSlot = number;

        if (savefile[number])
        { GoLobby(); }
        else
        { 
            Creat();
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
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

            GameManager.instance.nowPlayer.isJoy = true;
            GameManager.instance.nowPlayer.fixedJoy = true;
            GameManager.instance.nowPlayer.floatingJoy = false;
            GameManager.instance.nowPlayer.dynamicJoy = false;

            GameManager.instance.SavePlayerData();
            GameManager.instance.LoadPlayerData();
            GameManager.instance.LoadItemBase();
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            GameManager.instance.LoadPlayerData();
            GameManager.instance.LoadItemBase();
            SceneManager.LoadScene("Lobby");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        }
    }

    public void DeleteUiOpen(int number)
    {
        saveDeleteUi.SetActive(true);
        deleteBtn.onClick.AddListener(() => SaveDelete(number));
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void DeleteUiClose()
    {
        saveDeleteUi.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void SaveDelete(int index)
    {
        File.Delete(GameManager.instance.path + $"{index}");
        GameManager.instance.nowSlot = -1;
        savefile[index] = false;
        SlotUpdate();
        saveDeleteUi.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }
}
