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
    public GameObject gameOver;
    public GameObject gameClear;
    public Slider hpBar;

    public float setTime;

    private bool isStatus;
    private bool isSetting;

    void Start()
    {
        Player.onPlayerLevelUp += UpdatePlayerLevel;
        Player.onPlayerWaveUp += UpdateWave;
        Player.onGameOver += GameOverUiOpen;

        setTime = GameManager.Instance.setTime;
    }

    void OnDestroy()
    {
        Player.onPlayerLevelUp -= UpdatePlayerLevel;
        Player.onPlayerWaveUp -= UpdateWave;
        Player.onGameOver -= GameOverUiOpen;
    }

    void Update()
    {
        if (setTime > 0)
        {
            setTime -= Time.deltaTime;

            int min = (int)(setTime / 60);
            float sec = (setTime % 60);

            countdownTimer.text = min.ToString("D2") + ":" + sec.ToString("00.00");
        }
        else if (setTime <= 0)
        {
            gameClear.SetActive(true);
            Time.timeScale = 0;
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Win);
        }

        expBar.maxValue = GameManager.Instance.player.playerMaxExp;
        expBar.value = GameManager.Instance.player.playerExp;

        hpBar.maxValue = GameManager.Instance.player.playerMaxHp;
        hpBar.value = GameManager.Instance.player.playerHp;
    }

    public void OpenInventory()
    {
        isStatus = !isStatus;
        if (isStatus)
        {
            status.SetActive(true);
            AudioManager.Instance.EffectBgm(true);
            Time.timeScale = 0;
        }
        else
        {
            status.SetActive(false);

            if (!levelUp.activeSelf)
            {
                AudioManager.Instance.EffectBgm(false);
                Time.timeScale = 1.0f;
            }
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
            AudioManager.Instance.EffectBgm(true);
            Time.timeScale = 0;
        }
        else
        {
            setting.SetActive(false);

            if (!levelUp.activeSelf)
            {
                AudioManager.Instance.EffectBgm(false);
                Time.timeScale = 1.0f;
            }
        }
        isStatus = false;
        status.SetActive(false);
    }

    void GameOverUiOpen()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    void UpdatePlayerLevel()
    {
        playerLevel.text = "Lv." + GameManager.Instance.player.playerLevel;
    }

    void UpdateWave()
    {
        wave.text = "Wave." + GameManager.Instance.player.currentWave;
    }
}