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

    public bool isSetting = false;

    public Button artifactPrefab;
    public Button[] artifactBtns;
    public GameObject Content;

    public Text[] descriptions;

    public void GameStart()
    {
        SceneManager.LoadScene("InGame");
    }

    public void UpgradeOpen()
    {
        upgradeUi.SetActive(true);
        menuUi.SetActive(false);
        closeToUi = upgradeUi.gameObject;
    }

    public void ArtifactUiOpen()
    {
        artifactUi.SetActive(true);
        menuUi.SetActive(false);
        closeToUi = artifactUi.gameObject;

        for (int i = 0; i < GameManager.itemList.Count; i++)
        {
            GameObject newButton = Instantiate(artifactPrefab.gameObject, Content.transform);
            Button buttonComponent = newButton.GetComponent<Button>();
            artifactBtns[i] = buttonComponent;

            Text index = newButton.GetComponentInChildren<Text>();
            index.text = (i + 1).ToString("00");

            int indexCopy = i;
            buttonComponent.onClick.AddListener(() => DescriptionsOpen(indexCopy));
        }
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

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
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
        }
    }
}
