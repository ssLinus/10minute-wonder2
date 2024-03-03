using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerHp;
    public float playerSpeed;
    public Rigidbody2D playerRB;

    public Vector3 axis;

    void Start()
    {
        playerHp = GameManager.instance.playerHp;
        playerSpeed = GameManager.instance.playerSpeed;
        playerRB = GetComponent<Rigidbody2D>();
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
