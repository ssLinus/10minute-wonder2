using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text countdownTimer;
    public Text playerLevel;
    public Text wave;
    public Slider expBar;
    public GameObject levelUp;
    public GameObject status;
    public GameObject setting;
    public Slider hpBar;

    public float setTime;

    private bool isStatus;
    private bool isSetting;

    void Start()
    {
        Player.onPlayerLevelUp += UpdatePlayerLevel;
        Player.onPlayerWaveUp += UpdateWave;

        setTime = GameManager.instance.setTime;
    }

    void OnDestroy()
    {
        Player.onPlayerLevelUp -= UpdatePlayerLevel;
        Player.onPlayerWaveUp -= UpdateWave;
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

        expBar.maxValue = GameManager.instance.player.playerMaxExp;
        expBar.value = GameManager.instance.player.playerExp;

        hpBar.maxValue = GameManager.instance.player.playerMaxHp;
        hpBar.value = GameManager.instance.player.playerHp;
    }

    public void OpenInventory()
    {
        isStatus = !isStatus;
        if (isStatus)
        {
            status.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            status.SetActive(false);
            Time.timeScale = 1.0f;
        }
        isSetting = false;
        setting.SetActive(false);
    }

    public void OpenSetting()
    {
        isSetting = !isSetting;
        if (isSetting)
        {
            setting.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            setting.SetActive(false);
            Time.timeScale = 1.0f;
        }
        isStatus = false;
        status.SetActive(false);
    }

    public void OptionSelect(int index)
    {
        levelUp.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OptionReset()
    {

    }

    void UpdatePlayerLevel()
    {
        playerLevel.text = "Lv." + GameManager.instance.player.playerLevel;

        levelUp.SetActive(true);
        Time.timeScale = 0;
    }

    void UpdateWave()
    {
        wave.text = "Wave." + GameManager.instance.player.currentWave;
    }
}