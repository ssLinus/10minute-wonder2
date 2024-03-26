using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public GameObject levelUpUi;
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

        tempItemList = new List<int>(); // 임시 아이템 리스트 초기화

        if (GameManager.itemList.Count >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                int rand;
                do rand = Random.Range(0, GameManager.itemList.Count);
                while (tempItemList.Contains(rand)); // 이미 사용된 인덱스가 나올 경우, 다시 랜덤값 생성

                tempItemList.Add(rand); // 사용된 인덱스 리스트에 추가
                selectBtns[i].onClick.RemoveAllListeners(); // 초기화
                selectBtns[i].onClick.AddListener(() => OptionSelect(rand));

                string itemName = GameManager.itemList[rand].name;
                optionsName[i].text = itemName;
                switch (GameManager.itemList[rand].Grade)
                {
                    case "레어":
                        optionsName[i].color = Color.green;
                        break;
                    case "유니크":
                        optionsName[i].color = Color.red;
                        break;
                    case "에픽":
                        optionsName[i].color = Color.magenta;
                        break;
                    default:
                        optionsName[i].color = Color.black;
                        break;
                }

                descriptionTxt[i].text = "";

                var item = GameManager.itemList[rand];

                if (item.MaxHp != 0) descriptionTxt[i].text += "최대체력 : " + item.MaxHp + "\n";
                if (item.MoveSpeed != 0) descriptionTxt[i].text += "이동속도 : " + item.MoveSpeed + "\n";
                if (item.AttackDmg != 0) descriptionTxt[i].text += "공격력 : " + item.AttackDmg + "\n";
                if (item.AttackSpeed != 0) descriptionTxt[i].text += "공격속도 : " + item.AttackSpeed + "\n";
                if (item.AttackRange != 0) descriptionTxt[i].text += "공격범위 : " + item.AttackRange + "\n";
                if (item.BulletSpeed != 0) descriptionTxt[i].text += "탄환속도 : " + item.BulletSpeed + "\n";
                if (item.BulletLifeTime != 0) descriptionTxt[i].text += "유지시간 : " + item.BulletLifeTime + "\n";
                if (item.BulletPen != 0) descriptionTxt[i].text += "관통력 : " + item.BulletPen + "\n";
                if (item.LootingRange != 0) descriptionTxt[i].text += "획득범위 : " + item.LootingRange + "\n";
                if (item.ExpMultipler != 0) descriptionTxt[i].text += "Exp.* : " + item.ExpMultipler + "\n";
                if (item.Fire != 0) descriptionTxt[i].text += "화염 : " + item.Fire + "\n";
                if (item.Electric != 0) descriptionTxt[i].text += "전기 : " + item.Electric + "\n";
                if (item.Ice != 0) descriptionTxt[i].text += "얼음 : " + item.Ice + "\n";
                if (item.Poison != 0) descriptionTxt[i].text += "중독 : " + item.Poison + "\n";
            }
        }

        Time.timeScale = 0;

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.Instance.EffectBgm(true);
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
        GameManager.Instance.player.playerMaxHp += GameManager.itemList[rand].MaxHp;
        GameManager.Instance.player.playerHp += GameManager.itemList[rand].MaxHp;
        GameManager.Instance.player.playerSpeed += GameManager.itemList[rand].MoveSpeed;
        GameManager.Instance.looting.collider2D.radius += GameManager.itemList[rand].LootingRange;
        GameManager.Instance.attackDmg += GameManager.itemList[rand].AttackDmg;
        GameManager.Instance.bulletSpawner.attackSpeed += GameManager.itemList[rand].AttackSpeed;
        GameManager.Instance.bulletSpawner.collider2D.radius += GameManager.itemList[rand].AttackRange;
        GameManager.Instance.bulletSpeed += GameManager.itemList[rand].BulletSpeed;
        GameManager.Instance.bulletLifeTime += GameManager.itemList[rand].BulletLifeTime;
        GameManager.Instance.bulletPen += GameManager.itemList[rand].BulletPen;
        GameManager.Instance.expMultipler += GameManager.itemList[rand].ExpMultipler;
        GameManager.Instance.fire += GameManager.itemList[rand].Fire;
        GameManager.Instance.electric += GameManager.itemList[rand].Electric;
        GameManager.Instance.ice += GameManager.itemList[rand].Ice;
        GameManager.Instance.poison += GameManager.itemList[rand].Poison;

        GameManager.MyItem.Add(GameManager.itemList[rand]);
        GameManager.itemList.RemoveAt(rand);

        levelUpUi.SetActive(false);

        Time.timeScale = 1.0f;

        resetPoint = initialResetPoint;

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.Instance.EffectBgm(false);
    }
}
