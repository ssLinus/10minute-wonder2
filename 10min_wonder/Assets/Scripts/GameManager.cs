using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

    [Header("�ʱ� ����")]

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
            Debug.Log("�ı�");
        }

        // player ������ ��������� Player Ÿ���� ��ü�� ã�Ƽ� player ������ �Ҵ�
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }
}
