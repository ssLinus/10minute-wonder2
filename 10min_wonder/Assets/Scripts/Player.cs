using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("게임매니저")]
    public float playerMaxHp;
    public float playerSpeed;

    public float playerHp;

    public int playerLevel; // 0
    public float playerExp;
    public float playerMaxExp;
    public bool isLevelUp;

    public Rigidbody2D playerRB;

    public Vector3 axis;

    void Start()
    {
        playerMaxHp = GameManager.instance.playerMaxHp;
        playerSpeed = GameManager.instance.playerSpeed;
        playerRB = GetComponent<Rigidbody2D>();

        playerHp = playerMaxHp;

        isLevelUp = false;
    }

    void Update()
    {
        if (playerHp <= 0)
        {
            Time.timeScale = 0;
        }

        if (playerRB != null) // 플레이어 이동관련
        {
            axis.x = Input.GetAxisRaw("Horizontal");
            axis.y = Input.GetAxisRaw("Vertical");
        }

        if(playerExp >= playerMaxExp) // 플레이어 레벨 관련
        {
            playerLevel++;
            playerExp = (playerExp - playerMaxExp);
            playerMaxExp = playerMaxExp * (1 + 0.1f * playerLevel);
            isLevelUp = true;
        }
    }

    void FixedUpdate()
    {
        // 정규화된 벡터로 변환
        Vector3 movementDirection = new Vector3(axis.x, axis.y).normalized;

        // 각각의 축에 속도를 적용
        float hSpeed = movementDirection.x * playerSpeed;
        float vSpeed = movementDirection.y * playerSpeed;

        playerRB.velocity = new Vector3(hSpeed, vSpeed);
    }
}
