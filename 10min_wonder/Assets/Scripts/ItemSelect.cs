using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public GameObject levelUpUi;
    public Text[] optionsTxt;
    public Button[] optionBtns;

    List<int> tempItemList;

    public void Start()
    {
        Player.onPlayerLevelUp += OpenSelectUi;
    }

    public void OnDestroy()
    {
        Player.onPlayerLevelUp -= OpenSelectUi;
    }

    public void OpenSelectUi()
    {
        levelUpUi.SetActive(true);

        tempItemList = new List<int>(); // �ӽ� ������ ����Ʈ �ʱ�ȭ

        if (GameManager.itemList.Count >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                int rand;
                do rand = Random.Range(0, GameManager.itemList.Count);
                while (tempItemList.Contains(rand)); // �̹� ���� �ε����� ���� ���, �ٽ� ������ ����

                tempItemList.Add(rand); // ���� �ε��� ����Ʈ�� �߰�

                optionBtns[i].onClick.RemoveAllListeners(); // �ʱ�ȭ
                optionBtns[i].onClick.AddListener(() => OptionSelect(rand));

                string itemName = GameManager.itemList[rand].name;
                optionsTxt[i].text = itemName;
            }
        }
        Time.timeScale = 0;
    }

    private void OptionSelect(int rand)
    {
        print(GameManager.itemList[rand].name);

        GameManager.instance.player.playerMaxHp += GameManager.itemList[rand].MaxHp;
        GameManager.instance.player.playerHp += GameManager.itemList[rand].MaxHp;
        GameManager.instance.player.playerSpeed += GameManager.itemList[rand].MoveSpeed;
        GameManager.instance.looting.collider2D.radius += GameManager.itemList[rand].LootingRange;
        GameManager.instance.attackDmg += GameManager.itemList[rand].AttackDmg;
        GameManager.instance.bulletSpawner.attackSpeed += GameManager.itemList[rand].AttackSpeed;
        GameManager.instance.bulletSpawner.collider2D.radius += GameManager.itemList[rand].AttackRange;
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

        levelUpUi.SetActive(false);

        Time.timeScale = 1.0f;
    }
}
