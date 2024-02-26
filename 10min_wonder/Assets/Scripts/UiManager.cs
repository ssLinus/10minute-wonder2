using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject setting;

    private bool isInventory;
    private bool isSetting;

    void Start()
    {
        isInventory = false;
        isSetting = false;
    }


    void Update()
    {
        if (isInventory)
        { inventory.SetActive(true); }
        else
        { inventory.SetActive(false); }

        if (isSetting)
        { setting.SetActive(true); }
        else
        { setting.SetActive(false); }
    }

    public void OpenInventory()
    {
        if (!isInventory)
        {
            isInventory = true;
            isSetting = false;
            Time.timeScale = 0;
        }
        else
        {
            isInventory = false;
            Time .timeScale = 1.0f;
        }
    }

    public void OpenSetting()
    {
        if(!isSetting)
        {
            isInventory = false;
            isSetting = true;
            Time.timeScale = 0;
        }
        else
        {
            isSetting = false;
            Time.timeScale = 1.0f;
        }
    }
}
