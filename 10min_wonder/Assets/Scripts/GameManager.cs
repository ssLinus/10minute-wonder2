using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerData
{
    public string playerName;
    public float playerMaxHp;
    public float playerSpeed;
    public float attackDmg;
    public float attackSpeed;
    public float attackRange;
    public float bulletSpeed;
    public float bulletLifeTime;
    public float bulletPen;
    public float lootingRange;
    public float expMultipler;
    public float coin;

    public int[] upgradeSteps;
    public int[] artifactUnlock;
}

public class Item
{
    public int index;
    public string name;
    public float MaxHp;
    public float MoveSpeed;
    public float AttackDmg;
    public float AttackSpeed;
    public float AttackRange;
    public float BulletSpeed;
    public float BulletLifeTime;
    public float BulletPen;
    public float LootingRange;
    public float ExpMultipler;
    public int Fire;
    public int Electric;
    public int Ice;
    public int Poison;
    public string Grade;

    public Item(int index, string name, float maxHp, float moveSpeed, float attackDmg, float attackSpeed, float attackRange, float bulletSpeed, float bulletLifeTime, float bulletPen, float lootingRange, float expMultipler, int fire, int electric, int ice, int poison, string grade)
    {
        this.index = index;
        this.name = name;
        MaxHp = maxHp;
        MoveSpeed = moveSpeed;
        AttackDmg = attackDmg;
        AttackSpeed = attackSpeed;
        AttackRange = attackRange;
        BulletSpeed = bulletSpeed;
        BulletLifeTime = bulletLifeTime;
        BulletPen = bulletPen;
        LootingRange = lootingRange;
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
    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(GameManager).Name);
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public string filename = "save";
    public int nowSlot;

    public static List<Item> itemList = new List<Item>();
    public static List<Item> MyItem = new List<Item>();

    JsonData itemDate; // 아이템 데이터 파싱용

    public Player player;

    public bool isGameStart = false;

    public float playerMaxHp;
    public float playerSpeed;
    public float attackDmg;
    public float attackSpeed;
    public float attackRange;
    public float bulletSpeed;
    public float bulletLifeTime;
    public float bulletPen;
    public float lootingRange;
    public float expMultipler;
    public int fire;
    public int electric;
    public int ice;
    public int poison;

    public int[] upgradeSteps;
    public int[] artifactUnlock;

    public float setTime;

    public int monsterKill;

    public void Awake()
    {
        // Singleton Pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Debug.Log("파괴");
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        path = Application.persistentDataPath + "/save";
    }

    public void SavePlayerData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadPlayerData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        SetPlayerStat();
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("InGame");

        AudioManager.instance.PlayBgm(true);
    }

    public void SetPlayerStat()
    {
        playerMaxHp = nowPlayer.playerMaxHp;
        playerSpeed = nowPlayer.playerSpeed;
        attackDmg = nowPlayer.attackDmg;
        attackSpeed = nowPlayer.attackSpeed;
        attackRange = nowPlayer.attackRange;
        bulletSpeed = nowPlayer.bulletSpeed;
        bulletLifeTime = nowPlayer.bulletLifeTime;
        bulletPen = nowPlayer.bulletPen;
        lootingRange = nowPlayer.lootingRange;
        expMultipler = nowPlayer.expMultipler;
        upgradeSteps = nowPlayer.upgradeSteps;
        artifactUnlock = nowPlayer.artifactUnlock;
}

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

    public void LoadItemBase()
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
}
