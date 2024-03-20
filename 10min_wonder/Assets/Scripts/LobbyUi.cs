using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUi : MonoBehaviour
{
    public GameObject upgradeUi;
    public GameObject itemBookUi;
    public GameObject settingUi;

    public bool isSetting = false;

    public void GameStart()
    {
        SceneManager.LoadScene("InGame");
    }

    public void UpgradeOpen()
    {
        upgradeUi.SetActive(true);
    }

    public void ItemBookOpen()
    {
        itemBookUi.SetActive(true);
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void SettingOpen()
    {
        if (!isSetting)
        {
            settingUi.SetActive(true);
            isSetting = true;
        }
        else
        {
            settingUi.SetActive(false);
            isSetting = false;
        }
    }
}
