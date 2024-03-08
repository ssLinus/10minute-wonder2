using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.Networking;

public class PlayerDate
{
    public float playerMaxHp;
    public float playerSpeed;
    public float lootingRange;
    public float attackDmg;
    public float attackSpeed;
    public float attackRange;
    public float bulletSpeed;
    public float bulletLifeTime;
    public float bulletPen;
    public float expMultipler;
    public int coin;
}

public class Item
{
    public int index;
    public string name;
    public float MaxHp;
    public float MoveSpeed;
    public float LootingRange;
    public float AttackDmg;
    public float AttackSpeed;
    public float AttackRange;
    public float BulletSpeed;
    public float BulletLifeTime;
    public float BulletPen;
    public float ExpMultipler;
    public int Fire;
    public int Electric;
    public int Ice;
    public int Poison;
    public string Grade;

    public Item(int index, string name, float maxHp, float moveSpeed, float lootingRange, float attackDmg, float attackSpeed, float attackRange, float bulletSpeed, float bulletLifeTime, float bulletPen, float expMultipler, int fire, int electric, int ice, int poison, string grade)
    {
        this.index = index;
        this.name = name;
        MaxHp = maxHp;
        MoveSpeed = moveSpeed;
        LootingRange = lootingRange;
        AttackDmg = attackDmg;
        AttackSpeed = attackSpeed;
        AttackRange = attackRange;
        BulletSpeed = bulletSpeed;
        BulletLifeTime = bulletLifeTime;
        BulletPen = bulletPen;
        ExpMultipler = expMultipler;
        Fire = fire;
        Electric = electric;
        Ice = ice;
        Poison = poison;
        Grade = grade;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static List<Item> itemList = new List<Item>();
    public static List<Item> MyItem = new List<Item>();

    JsonData itemDate; // 아이템 데이터 파싱용

    void ParsingJsonItem(JsonData name, List<Item> listItem)
    {
        for (int i = 0; i < name.Count; i++)
        {
            string tempIndex = name[i][0].ToString();
            string tempName = name[i][1].ToString();
            string tempMaxHp = name[i][2].ToString();
            string tempMoveSpeed = name[i][3].ToString();
            string tempLootingRange = name[i][4].ToString();
            string tempAttackDmg = name[i][5].ToString();
            string tempAttckSpeed = name[i][6].ToString();
            string tempAttackRange = name[i][7].ToString();
            string tempBulletSpeed = name[i][8].ToString();
            string tempBulletLifeTime = name[i][9].ToString();
            string tempBulletPen = name[i][10].ToString();
            string tempExpMultipler = name[i][11].ToString();
            string tempFire = name[i][12].ToString();
            string tempElectric = name[i][13].ToString();
            string tempIce = name[i][14].ToString();
            string tempPoison = name[i][15].ToString();
            string tempGrade = name[i][16].ToString();

            int tempIndex_ = int.Parse(tempIndex);
            float tempMaxHp_ = float.Parse(tempMaxHp);
            float tempMoveSpeed_ = float.Parse(tempMoveSpeed);
            float tempLootingRange_ = float.Parse(tempLootingRange);
            float tempAttackDmg_ = float.Parse(tempAttackDmg);
            float tempAttckSpeed_ = float.Parse(tempAttckSpeed);
            float tempAttackRange_ = float.Parse(tempAttackRange);
            float tempBulletSpeed_ = float.Parse(tempBulletSpeed);
            float tempBulletLifeTime_ = float.Parse(tempBulletLifeTime);
            float tempBulletPen_ = float.Parse(tempBulletPen);
            float tempExpMultipler_ = float.Parse(tempExpMultipler);
            int tempFire_ = int.Parse(tempFire);
            int tempElectric_ = int.Parse(tempElectric);
            int tempIce_ = int.Parse(tempIce);
            int tempPoison_ = int.Parse(tempPoison);

            Item tempItem
                = new Item(tempIndex_, tempName, tempMaxHp_, tempMoveSpeed_,
                tempLootingRange_, tempAttackDmg_, tempAttckSpeed_, tempAttackRange_,
                tempBulletSpeed_, tempBulletLifeTime_, tempBulletPen_, tempExpMultipler_,
                tempFire_, tempElectric_, tempIce_, tempPoison_, tempGrade);
            listItem.Add(tempItem);
        }
    }

    public void LoadBase()
    {
        string Jsonstring;
        string filePath;

        filePath = Application.streamingAssetsPath + "/ItemList.json";

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest reader = new UnityWebRequest(filePath);
            while (!reader.isDone) { }
            Jsonstring = reader.downloadHandler.text;
        }
        else
        {
            Jsonstring = File.ReadAllText(filePath);
        }
        itemDate = JsonMapper.ToObject(Jsonstring);
        ParsingJsonItem(itemDate, itemList);
    }

    public Player player;
    public Looting looting;
    public BulletSpawner bulletSpawner;

    [Header("초기 설정")]

    [Header("Player")]
    public float playerMaxHp; // 100
    public float playerSpeed; // 3

    [Header("Looting")]
    public float lootingRange; // 0.5

    [Header("Monster")]
    public float attackDmg; // 2

    [Header("BulletSpawner")]
    public float attackSpeed; // 1
    public float attackRange; // 5

    [Header("Bullet")]
    public float bulletSpeed; // 5
    public float bulletLifeTime; // 2
    public float bulletPen; // 0
    public int fire;
    public int electric;
    public int ice;
    public int poison;

    [Header("Exp")]
    public float expMultipler; // 1

    [Header("MonsterSpawner")]
    public int startWave; // 0

    [Header("UiManager")]
    public float setTime; // 600

    private void Awake()
    {
        // Singleton Pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("파괴");
        }

        LoadBase();

        player = FindObjectOfType<Player>();
        looting = FindObjectOfType<Looting>();
        bulletSpawner = FindObjectOfType<BulletSpawner>();
    }
}
