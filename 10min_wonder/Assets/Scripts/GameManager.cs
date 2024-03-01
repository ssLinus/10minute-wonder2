using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton Pattern
    public static GameManager instance;
    private void Awake()
    {
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
    }

    public Player player;

    public float playerHp;
    public float playerSpeed;
    public float attackDmg; // 공격력
    public float attackSpeed; // 초당 공격속도
    public float bulletSpeed; // 탄 속도
    public float bulletLifeTime; // 탄 유지시간
    public float bulletPen; // 탄 관통력
    public float playerLevel; // 플레이어 레벨

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
