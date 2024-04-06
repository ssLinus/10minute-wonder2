using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUi : MonoBehaviour
{
    public Text countdownTimer;
    public Text playerLevel;
    public Text wave;
    public Slider expBar;
    public GameObject levelUp;
    public GameObject status;
    public GameObject setting;
    public GameObject gameOver;
    public Slider hpBar;

    public Text[] playerStatsText;
    public Text[] attributesText;

    public GameObject artifactPrefab;
    public Button[] artifactBtns;
    public GameObject inventoryContent;
    public GameObject descriptionUi;
    public Text[] descriptions;

    public GameObject attributeUi;
    public Text attributeIndex;
    public Text attributePoint;
    public Text attributeDescription;
    public Text[] Synergy;
    public Text[] SynergyDescriptions;

    public float setTime;

    private bool isStatus;
    private bool isSetting;
    private bool isGameOver = false;

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
        if (setTime >= 0)
        {
            setTime -= Time.deltaTime;

            int min = (int)(setTime / 60);
            float sec = (setTime % 60);

            countdownTimer.text = min.ToString("D2") + ":" + sec.ToString("00.00");
        }
        else
        {
            GameManager.instance.player.isGameClear = true;
        }

        if (GameManager.instance.player != null)
        {
            expBar.maxValue = GameManager.instance.player.playerMaxExp;
            expBar.value = GameManager.instance.player.playerExp;

            hpBar.maxValue = GameManager.instance.player.playerMaxHp;
            hpBar.value = GameManager.instance.player.playerHp;
        }

        if ((!GameManager.instance.player.isLive || GameManager.instance.player.isGameClear) && !isGameOver)
        {
            isGameOver = true;
            gameOver.SetActive(true);
        }
    }

    public void OpenInventory()
    {
        isStatus = !isStatus;
        if (isStatus)
        {
            status.SetActive(true);
            AudioManager.instance.EffectBgm(true);
            Time.timeScale = 0;
            PlayerStat();
            InventoryUi();
        }
        else
        {
            status.SetActive(false);

            if (!levelUp.activeSelf)
            {
                AudioManager.instance.EffectBgm(false);

                if (GameManager.instance.player.isLive)
                    Time.timeScale = 1.0f;
            }
        }
        isSetting = false;
        setting.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void PlayerStat()
    {
        playerStatsText[0].text = GameManager.instance.player.playerMaxHp.ToString("00");
        playerStatsText[1].text = GameManager.instance.player.playerSpeed.ToString("0.0");
        playerStatsText[2].text = GameManager.instance.attackDmg.ToString("0.0");
        playerStatsText[3].text = GameManager.instance.player.bulletSpawner.attackSpeed.ToString("0.0");
        playerStatsText[4].text = GameManager.instance.player.bulletSpawner.collider2D.radius.ToString("0.0");
        playerStatsText[5].text = GameManager.instance.bulletSpeed.ToString("0.0");
        playerStatsText[6].text = GameManager.instance.bulletLifeTime.ToString("0.0");
        playerStatsText[7].text = GameManager.instance.bulletPen.ToString("0");
        playerStatsText[8].text = GameManager.instance.player.looting.collider2D.radius.ToString("0.0");
        playerStatsText[9].text = GameManager.instance.expMultipler.ToString("0.0");

        attributesText[0].text = "화염 " + GameManager.instance.fire.ToString();
        attributesText[1].text = "전기 " + GameManager.instance.electric.ToString();
        attributesText[2].text = "얼음 " + GameManager.instance.ice.ToString();
        attributesText[3].text = "중독 " + GameManager.instance.poison.ToString();
    }

    public void InventoryUi()
    {
        foreach (Transform child in inventoryContent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < GameManager.MyItem.Count; i++)
        {
            GameObject newButton = Instantiate(artifactPrefab.gameObject, inventoryContent.transform);
            Button buttonComponent = newButton.GetComponent<Button>();
            artifactBtns[i] = buttonComponent;

            Text index = newButton.GetComponentInChildren<Text>();
            index.text = GameManager.MyItem[i].index.ToString("00");

            int indexCopy = i;

            GameObject unlock = newButton.transform.Find("Unlock").gameObject;
            unlock.SetActive(false);
            buttonComponent.onClick.AddListener(() => DescriptionsOpen(indexCopy));
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    private void DescriptionsOpen(int itemIndex)
    {
        var item = GameManager.MyItem[itemIndex];

        descriptions[0].text = item.index.ToString("00");
        descriptions[1].text = item.name;
        descriptions[2].text = item.Grade;

        string attributeText = "";
        if (item.Fire != 0) attributeText += "화염 ";
        if (item.Electric != 0) attributeText += "전기 ";
        if (item.Ice != 0) attributeText += "얼음 ";
        if (item.Poison != 0) attributeText += "중독 ";
        if (item.Fire + item.Electric + item.Ice + item.Poison == 0) attributeText = "무속성";
        descriptions[3].text = attributeText.Trim();

        string statText = "";
        string statText2 = "";

        AddStatText("최대체력", item.MaxHp, ref statText, ref statText2);
        AddStatText("이동속도", item.MoveSpeed, ref statText, ref statText2);
        AddStatText("공격력", item.AttackDmg, ref statText, ref statText2);
        AddStatText("공격속도", item.AttackSpeed, ref statText, ref statText2);
        AddStatText("공격범위", item.AttackRange, ref statText, ref statText2);
        AddStatText("탄환속도", item.BulletSpeed, ref statText, ref statText2);
        AddStatText("유지시간", item.BulletLifeTime, ref statText, ref statText2);
        AddStatText("관통력", item.BulletPen, ref statText, ref statText2);
        AddStatText("획득범위", item.LootingRange, ref statText, ref statText2);
        AddStatText("Exp.*", item.ExpMultipler, ref statText, ref statText2);

        descriptions[4].text = statText.Trim();
        descriptions[5].text = statText2.Trim();

        descriptionUi.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    void AddStatText(string statName, float statValue, ref string text1, ref string text2)
    {
        if (statValue != 0)
        {
            if (text1.Split('\n').Length <= 5)
            {
                text1 += $"{statName} {statValue}\n";
            }
            else
            {
                text2 += $"{statName} {statValue}\n";
            }
        }
    }

    public void CloseDescription()
    {
        descriptionUi.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void AttributeUiOpne(int i)
    {
        attributeUi.SetActive(true);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);

        int Count = 0;
        string attributeText = "";
        string descriptionText = "";

        switch (i)
        {
            case 0:
                Count = GameManager.instance.fire;
                attributeText = "화염";
                descriptionText = "투사체가 몬스터와 충돌하면 일정 범위 내에서 폭발이 발생합니다. \n폭발은 공격력의 절반에 해당하는 피해를 입힙니다.";
                break;
            case 1:
                Count = GameManager.instance.electric;
                attributeText = "전기";
                descriptionText = "투사체가 몬스터와 충돌하면 가장 가까운 몬스터를 향해 일정 횟수 튕깁니다. \n이 효과는 관통력에 영향을 받습니다.";
                break;
            case 2:
                Count = GameManager.instance.ice;
                attributeText = "얼음";
                descriptionText = "투사체가 몬스터와 충돌하면 몬스터를 일정 시간동안 빙결시킵니다. \n빙결 시간은 중첩되지 않습니다.";
                break;
            case 3:
                Count = GameManager.instance.poison;
                attributeText = "중독";
                descriptionText = "투사체가 몬스터와 충돌하면 몬스터를 5초 동안 중독시킵니다. \n중독은 일정한 간격으로 공격력의 1/3에 해당하는 피해를 입힙니다.";
                break;
        }

        attributeIndex.text = attributeText;
        attributePoint.text = Count.ToString("00");
        attributeDescription.text = descriptionText;

        for (int j = 0; j < SynergyDescriptions.Length; j++)
        {
            SynergyDescriptions[j].text = GetSynergyText(i, j);
        }

        SetSynergyColor(Count);
    }

    private string GetSynergyText(int attributeIndex, int synergyIndex)
    {
        switch (attributeIndex)
        {
            case 0: // 화염
                return "폭발 범위 " + (10 + 5 * synergyIndex);
            case 1: // 전기
                return "튕김 횟수 " + (synergyIndex + 1);
            case 2: // 얼음
                return "빙결 시간 " + (0.5 + synergyIndex * 0.25);
            case 3: // 중독
                return "피해 간격 " + (1 - synergyIndex * 0.25);
            default:
                return "";
        }
    }

    private void SetSynergyColor(int count)
    {
        Color textColor;
        if (count >= 7)
        { textColor = Color.magenta; }
        else if (count >= 5)
        { textColor = Color.magenta; }
        else if (count >= 3)
        { textColor = Color.magenta; }
        else
        { textColor = Color.black; }

        // Apply color to the appropriate indices
        if (count >= 7)
        {
            Synergy[2].color = textColor;
            SynergyDescriptions[2].color = textColor;
        }
        else if (count >= 5)
        {
            Synergy[1].color = textColor;
            SynergyDescriptions[1].color = textColor;
        }
        else if (count >= 3)
        {
            Synergy[0].color = textColor;
            SynergyDescriptions[0].color = textColor;
        }
    }


    public void AttributeClose()
    {
        attributeUi.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);

        for (int i = 0; i < Synergy.Length; i++)
        {
            Synergy[i].color = Color.black;
            SynergyDescriptions[i].color = Color.black;
        }
    }

    public void OpenSetting()
    {
        isSetting = !isSetting;
        if (isSetting)
        {
            setting.SetActive(true);
            AudioManager.instance.EffectBgm(true);
            Time.timeScale = 0;
        }
        else
        {
            setting.SetActive(false);

            GameManager.instance.SetJoystick();
            GameManager.instance.SavePlayerData();

            GameManager.instance.player.ActiveJoystick();

            if (!levelUp.activeSelf)
            {
                AudioManager.instance.EffectBgm(false);
                if (GameManager.instance.player.isLive)
                    Time.timeScale = 1.0f;
            }
        }
        isStatus = false;
        status.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    void UpdatePlayerLevel()
    {
        playerLevel.text = "Lv." + GameManager.instance.player.playerLevel;
    }

    void UpdateWave()
    {
        wave.text = "Wave." + GameManager.instance.player.currentWave;
    }

    public void GoToTheLobby()
    {
        SceneManager.LoadScene("Lobby");
        GameManager.instance.ItemListClear();
        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Time.timeScale = 1;
    }
}