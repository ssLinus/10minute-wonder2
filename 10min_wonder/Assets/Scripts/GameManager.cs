using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    [Header("초기 설정")]

    [Header("Player")]
    public float playerMaxHp; // 100
    public float playerSpeed; // 3
    public int playerLevel; // 0

    [Header("Looting")]
    public float lootingRange; // 0.5

    [Header("BulletSpawner")]
    public float attackDmg; // 2
    public float attackSpeed; // 1
    public float attackRange; // 5

    [Header("Bullet")]
    public float bulletSpeed; // 5
    public float bulletLifeTime; // 2
    public float bulletPen; // 0

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

        // player 변수가 비어있으면 Player 타입의 객체를 찾아서 player 변수에 할당
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }
}
