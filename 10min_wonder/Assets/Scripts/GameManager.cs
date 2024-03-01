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
            Debug.Log("�ı�");
        }
    }

    public Player player;

    public float playerHp;
    public float playerSpeed;
    public float attackDmg; // ���ݷ�
    public float attackSpeed; // �ʴ� ���ݼӵ�
    public float bulletSpeed; // ź �ӵ�
    public float bulletLifeTime; // ź �����ð�
    public float bulletPen; // ź �����
    public float playerLevel; // �÷��̾� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
