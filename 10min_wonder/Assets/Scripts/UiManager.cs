using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text countdownTimer;
    public Text playerLevel;
    public Text Score;
    public Slider expBar;
    public GameObject levelUp;
    public GameObject status;
    public GameObject setting;

    public float setTime; // 10Ка, 600УЪ

    private bool isStatus;
    private bool isSetting;

    void Start()
    {
        isStatus = false;
        isSetting = false;
    }

    void Update()
    {
        if (setTime > 0)
        {
            setTime -= Time.deltaTime;

            int min = (int)(setTime / 60);
            float sec = (setTime % 60);

            countdownTimer.text = min.ToString("D2") + ":" + sec.ToString("N2");

            if (setTime < 10)
                countdownTimer.text = "00:0" + sec.ToString("N2");
        }
        else if (setTime <= 0)
            Time.timeScale = 0;

        if (isStatus)
            status.SetActive(true);
        else
            status.SetActive(false);

        if (isSetting)
            setting.SetActive(true);
        else
            setting.SetActive(false);
    }

    public void OpenInventory()
    {
        if (!isStatus)
        {
            isStatus = true;
            isSetting = false;
            Time.timeScale = 0;
        }
        else
        {
            isStatus = false;
            Time.timeScale = 1.0f;
        }
    }

    public void OpenSetting()
    {
        if (!isSetting)
        {
            isStatus = false;
            isSetting = true;
            Time.timeScale = 0;
        }
        else
        {
            isSetting = false;
            Time.timeScale = 1.0f;
        }
    }

    public void OptionSelect(int index) // index : 0 ~ 2
    {
        levelUp.SetActive(false);
    }

    public void OptionReset()
    {

    }
}
