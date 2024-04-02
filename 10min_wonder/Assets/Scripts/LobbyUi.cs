using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUi : MonoBehaviour
{
    public GameObject menuUi;
    public GameObject upgradeUi;
    public GameObject artifactUi;
    public GameObject settingUi;
    public GameObject descriptionUi;
    public GameObject closeToUi;
    public Text nickName;

    public bool isSetting = false;

    [Header("Upgrade")]
    public GameObject upgradePrefab;
    public GameObject upgradeContent;
    public Sprite[] icons;
    public Text[] requiredCoin;
    public GameObject upgradePoint;

    public Text playerStatText;
    public Text coin;
    public GameObject costIssueUi;
    public GameObject upgradeStepIssueUi;

    [Header("Artifact")]
    public Button artifactPrefab;
    public Button[] artifactBtns;
    public GameObject content;
    public Text[] descriptions;
    public GameObject unlockIssueUi;


    public void Start()
    {
        nickName.text = GameManager.instance.nowPlayer.playerName;
    }

    public void GameStart()
    {
        GameManager.instance.GameStart();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void UpgradeOpen()
    {
        foreach (Transform child in upgradeContent.transform)
        {
            Destroy(child.gameObject);
        }

        upgradeUi.SetActive(true);
        menuUi.SetActive(false);
        closeToUi = upgradeUi.gameObject;

        for (int i = 0; i < 10; i++)
        {
            GameObject upgrade = Instantiate(upgradePrefab, upgradeContent.transform);

            // 아이콘
            Transform iconTransform = upgrade.transform.Find("Icon");
            GameObject icon = iconTransform.gameObject;
            Image spriteImage = icon.GetComponent<Image>();
            spriteImage.sprite = icons[i];

            // 비용
            Transform costTextTransform = upgrade.transform.Find("Cost");
            Text costText = costTextTransform.GetComponentInChildren<Text>();
            requiredCoin[i] = costText;

            // 증가량
            Transform increaseTextTransform = upgrade.transform.Find("Increase");
            Text increaseText = increaseTextTransform.GetComponent<Text>();
            string[] increaseTexts = { "+50", "+1", "+2", "+0.5", "+0.5", "+0.5", "+0.5", "+1", "+0.5", "+0.2" };
            increaseText.text = increaseTexts[i];

            // 업그레이드 단계
            Transform[] upgradePoints = new Transform[3];
            for (int j = 0; j < 3; j++)
            {
                upgradePoints[j] = upgrade.transform.Find($"Upgrade ({j + 1})");
            }
            for (int index = 0; index < GameManager.instance.nowPlayer.upgradeSteps[i] && index < 3; index++)
            {
                Transform targetTransform = upgradePoints[index];
                if (targetTransform != null)
                {
                    Instantiate(upgradePoint, targetTransform);
                }
            }

            // 업그레이드 버튼
            Button upgradeBtn = upgrade.GetComponentInChildren<Button>();
            int number = i;
            upgradeBtn.onClick.AddListener(() => StatUpgrade(number));
        }

        PlayerStatUpdate();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void StatUpgrade(int index)
    {
        int cost = 100 * (GameManager.instance.nowPlayer.upgradeSteps[index] + 1);

        if (GameManager.instance.nowPlayer.upgradeSteps[index] < 2 && GameManager.instance.nowPlayer.coin >= cost)
        {
            switch (index)
            {
                case 0: // 최대체력
                    GameManager.instance.nowPlayer.playerMaxHp += 50;
                    break;
                case 1: // 이동속도
                    GameManager.instance.nowPlayer.playerSpeed += 1;
                    break;
                case 2: // 공격력
                    GameManager.instance.nowPlayer.attackDmg += 2;
                    break;
                case 3: // 공격속도
                    GameManager.instance.nowPlayer.attackSpeed += 0.5f;
                    break;
                case 4: // 공격범위
                    GameManager.instance.nowPlayer.attackRange += 0.5f;
                    break;
                case 5: // 투사체속도
                    GameManager.instance.nowPlayer.bulletSpeed += 0.5f;
                    break;
                case 6: // 유지시간
                    GameManager.instance.nowPlayer.bulletLifeTime += 0.5f;
                    break;
                case 7: // 관통력
                    GameManager.instance.nowPlayer.bulletPen += 1;
                    break;
                case 8: // 획득범위
                    GameManager.instance.nowPlayer.lootingRange += 0.5f;
                    break;
                case 9: // 경험치 배율
                    GameManager.instance.nowPlayer.expMultipler += 0.2f;
                    break;
            }

            GameManager.instance.nowPlayer.upgradeSteps[index]++;
            GameManager.instance.nowPlayer.coin -= cost;
            UpgradeOpen();
        }
        else if (GameManager.instance.nowPlayer.upgradeSteps[index] == 2)
        {
            // 최대단계
            upgradeStepIssueUi.SetActive(true);
        }
        else if (GameManager.instance.nowPlayer.coin < cost)
        {
            // 비용부족
            costIssueUi.SetActive(true);
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void PlayerStatUpdate()
    {
        playerStatText.text = string.Format(
            "{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}\n{9}",
            GameManager.instance.nowPlayer.playerMaxHp,
            GameManager.instance.nowPlayer.playerSpeed,
            GameManager.instance.nowPlayer.attackDmg,
            GameManager.instance.nowPlayer.attackSpeed,
            GameManager.instance.nowPlayer.attackRange,
            GameManager.instance.nowPlayer.bulletSpeed,
            GameManager.instance.nowPlayer.bulletLifeTime,
            GameManager.instance.nowPlayer.bulletPen,
            GameManager.instance.nowPlayer.lootingRange,
            GameManager.instance.nowPlayer.expMultipler);

        coin.text = GameManager.instance.nowPlayer.coin.ToString();

        for (int i = 0; i < GameManager.instance.nowPlayer.upgradeSteps.Length; i++)
        {
            requiredCoin[i].text = (100 + 100 * GameManager.instance.nowPlayer.upgradeSteps[i]).ToString();
        }

        GameManager.instance.SavePlayerData();
        GameManager.instance.LoadPlayerData();
    }


    public void ArtifactUiOpen()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        artifactUi.SetActive(true);
        menuUi.SetActive(false);
        closeToUi = artifactUi.gameObject;

        for (int i = 0; i < GameManager.itemList.Count; i++)
        {
            GameObject newButton = Instantiate(artifactPrefab.gameObject, content.transform);
            Button buttonComponent = newButton.GetComponent<Button>();
            artifactBtns[i] = buttonComponent;

            Text index = newButton.GetComponentInChildren<Text>();
            index.text = (i + 1).ToString("00");

            int indexCopy = i;

            GameObject unlock = newButton.transform.Find("Unlock").gameObject;

            if (GameManager.instance.artifactUnlock[indexCopy] != 0)
            {
                unlock.SetActive(false);
                buttonComponent.onClick.AddListener(() => DescriptionsOpen(indexCopy));
            }
            else
            {
                // 미해금
                buttonComponent.onClick.AddListener(() => UnlockUiOpen());
            }
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    private void DescriptionsOpen(int itemIndex)
    {
        var item = GameManager.itemList[itemIndex];

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
        closeToUi = descriptionUi;
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

    public void UnlockUiOpen()
    {
        unlockIssueUi.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void CloseUi()
    {
        if (closeToUi == descriptionUi)
        {
            closeToUi.SetActive(false);
            closeToUi = artifactUi;
        }
        else if (closeToUi != null)
        {
            closeToUi.SetActive(false);
            closeToUi = null;
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        menuUi.SetActive(true);
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
            GameManager.instance.SetJoystick();
            GameManager.instance.SavePlayerData();
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void TutorialOpen()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
