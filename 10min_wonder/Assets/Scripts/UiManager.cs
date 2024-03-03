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

    public float setTime; // 10분, 600초

    private bool isStatus;
    private bool isSetting;

    void Start()
    {
        isStatus = false;
        isSetting = false;
    }

    void Update()
    {
        // 카운트다운
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

        // 플레이어 레벨
        playerLevel.text = "Lv." + GameManager.instance.player.playerLevel;

        // 플레이어 경험치 바
        expBar.maxValue = GameManager.instance.player.playerMaxExp;
        expBar.value = GameManager.instance.player.playerExp;

        // Wave
        //MonsterSpawner monsterSpawner = GetComponent<MonsterSpawner>();
        //wave.text = "Wave." + monsterSpawner.wave;

        // 레벨업 창
        if (GameManager.instance.player.isLevelUp)
        {
            GameManager.instance.player.isLevelUp = false;
            levelUp.SetActive(true);
            Time.timeScale = 0;
        }

        // 스탯창
        if (isStatus)
            status.SetActive(true);
        else
            status.SetActive(false);

        // 설정창
        if (isSetting)
            setting.SetActive(true);
        else
            setting.SetActive(false);

        // HP Bar
        hpBar.maxValue = GameManager.instance.player.playerMaxHp;
        hpBar.value = GameManager.instance.player.playerHp;
    }

    public void OpenInventory() // 인벤토리
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

    public void OpenSetting() // 설정창
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
        Time.timeScale = 1.0f;
    }

    public void OptionReset()
    {

    }
}
