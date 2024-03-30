using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public GameObject levelUpUi;
    public Text[] optionsIndex;
    public Text[] optionsName;
    public Text[] descriptionTxt;
    public Button[] selectBtns;
    public Text resetText;
    public int resetPoint;
    public int initialResetPoint;

    List<int> tempItemList;

    public void Start()
    {
        Player.onPlayerLevelUp += OpenSelectUi;

        resetPoint = initialResetPoint;
    }

    public void OnDestroy()
    {
        Player.onPlayerLevelUp -= OpenSelectUi;
    }

    public void OpenSelectUi()
    {
        levelUpUi.SetActive(true);

        resetText.text = "Reset(" + resetPoint + ")";

        tempItemList = new List<int>(); // �ӽ� ������ ����Ʈ �ʱ�ȭ

        if (GameManager.itemList.Count >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                int rand;
                do rand = Random.Range(0, GameManager.itemList.Count);
                while (tempItemList.Contains(rand)); // �̹� ���� �ε����� ���� ���, �ٽ� ������ ����

                tempItemList.Add(rand); // ���� �ε��� ����Ʈ�� �߰�
                selectBtns[i].onClick.RemoveAllListeners(); // �ʱ�ȭ
                selectBtns[i].onClick.AddListener(() => OptionSelect(rand));

                optionsIndex[i].text = GameManager.itemList[rand].index.ToString("00");

                string itemName = GameManager.itemList[rand].name;
                optionsName[i].text = GameManager.itemList[rand].name;
                switch (GameManager.itemList[rand].Grade)
                {
                    case "����":
                        optionsName[i].color = Color.green;
                        break;
                    case "����ũ":
                        optionsName[i].color = Color.red;
                        break;
                    case "����":
                        optionsName[i].color = Color.magenta;
                        break;
                    default:
                        optionsName[i].color = Color.black;
                        break;
                }

                descriptionTxt[i].text = "";

                var item = GameManager.itemList[rand];

                if (item.MaxHp != 0) descriptionTxt[i].text += "�ִ�ü�� : " + item.MaxHp + "\n";
                if (item.MoveSpeed != 0) descriptionTxt[i].text += "�̵��ӵ� : " + item.MoveSpeed + "\n";
                if (item.AttackDmg != 0) descriptionTxt[i].text += "���ݷ� : " + item.AttackDmg + "\n";
                if (item.AttackSpeed != 0) descriptionTxt[i].text += "���ݼӵ� : " + item.AttackSpeed + "\n";
                if (item.AttackRange != 0) descriptionTxt[i].text += "���ݹ��� : " + item.AttackRange + "\n";
                if (item.BulletSpeed != 0) descriptionTxt[i].text += "źȯ�ӵ� : " + item.BulletSpeed + "\n";
                if (item.BulletLifeTime != 0) descriptionTxt[i].text += "�����ð� : " + item.BulletLifeTime + "\n";
                if (item.BulletPen != 0) descriptionTxt[i].text += "����� : " + item.BulletPen + "\n";
                if (item.LootingRange != 0) descriptionTxt[i].text += "ȹ����� : " + item.LootingRange + "\n";
                if (item.ExpMultipler != 0) descriptionTxt[i].text += "Exp.* : " + item.ExpMultipler + "\n";
                if (item.Fire != 0) descriptionTxt[i].text += "ȭ�� : " + item.Fire + "\n";
                if (item.Electric != 0) descriptionTxt[i].text += "���� : " + item.Electric + "\n";
                if (item.Ice != 0) descriptionTxt[i].text += "���� : " + item.Ice + "\n";
                if (item.Poison != 0) descriptionTxt[i].text += "�ߵ� : " + item.Poison + "\n";
            }
        }

        Time.timeScale = 0;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void OptionReset()
    {
        if (resetPoint > 0)
        {
            resetPoint--;
            levelUpUi.SetActive(false);
            OpenSelectUi();
        }
    }

    private void OptionSelect(int rand)
    {
        GameManager.instance.player.playerMaxHp += GameManager.itemList[rand].MaxHp;
        GameManager.instance.player.playerHp += GameManager.itemList[rand].MaxHp;
        GameManager.instance.player.playerSpeed += GameManager.itemList[rand].MoveSpeed;
        GameManager.instance.player.looting.collider2D.radius += GameManager.itemList[rand].LootingRange;
        GameManager.instance.attackDmg += GameManager.itemList[rand].AttackDmg;
        GameManager.instance.player.bulletSpawner.attackSpeed += GameManager.itemList[rand].AttackSpeed;
        GameManager.instance.player.bulletSpawner.collider2D.radius += GameManager.itemList[rand].AttackRange;
        GameManager.instance.bulletSpeed += GameManager.itemList[rand].BulletSpeed;
        GameManager.instance.bulletLifeTime += GameManager.itemList[rand].BulletLifeTime;
        GameManager.instance.bulletPen += GameManager.itemList[rand].BulletPen;
        GameManager.instance.expMultipler += GameManager.itemList[rand].ExpMultipler;
        GameManager.instance.fire += GameManager.itemList[rand].Fire;
        GameManager.instance.electric += GameManager.itemList[rand].Electric;
        GameManager.instance.ice += GameManager.itemList[rand].Ice;
        GameManager.instance.poison += GameManager.itemList[rand].Poison;

        GameManager.MyItem.Add(GameManager.itemList[rand]);
        GameManager.itemList.RemoveAt(rand);
        if (GameManager.instance.nowPlayer.artifactUnlock[rand] == 0)
        {
            GameManager.instance.nowPlayer.artifactUnlock[rand]++;
            GameManager.instance.SavePlayerData();
        }

        levelUpUi.SetActive(false);

        Time.timeScale = 1.0f;

        resetPoint = initialResetPoint;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }
}
