using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    [Header("Initial Stat")]
    public float playerMaxHp; // 100
    public float playerSpeed; // 3
    public float attackDmg; // 공격력 2
    public float attackSpeed; // 초당 공격속도 1
    public float bulletSpeed; // 탄 속도 5
    public float bulletLifeTime; // 탄 유지시간 2
    public float bulletPen; // 탄 관통력 0

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

        // player 변수가 비어있으면 Player 타입의 객체를 찾아서 player 변수에 할당
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
