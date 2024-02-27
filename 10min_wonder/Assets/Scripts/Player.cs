using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float playerHp;
    private float playerSpeed;
    private Rigidbody2D playerRB;
    private Vector3 axis;

    void Start()
    {
        playerHp = GameManager.instance.playerHp;
        playerSpeed = GameManager.instance.playerSpeed;
        playerRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (playerRB != null) // 플레이어 이동관련
        {
            axis.x = Input.GetAxis("Horizontal");
            axis.y = Input.GetAxis("Vertical");

            // 정규화된 벡터로 변환
            Vector3 movementDirection = new Vector3(axis.x, axis.y).normalized;

            // 각각의 축에 속도를 적용
            float hSpeed = movementDirection.x * playerSpeed;
            float vSpeed = movementDirection.y * playerSpeed;

            playerRB.velocity = new Vector3(hSpeed, vSpeed);
        }
    }
}
