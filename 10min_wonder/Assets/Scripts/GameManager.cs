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
    public float attackDmg; // ���ݷ� 2
    public float attackSpeed; // �ʴ� ���ݼӵ� 1
    public float bulletSpeed; // ź �ӵ� 5
    public float bulletLifeTime; // ź �����ð� 2
    public float bulletPen; // ź ����� 0

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

    void Start()
    {

    }

    void Update()
    {
        
    }
}
