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
        if (playerRB != null) // �÷��̾� �̵�����
        {
            axis.x = Input.GetAxis("Horizontal");
            axis.y = Input.GetAxis("Vertical");

            // ����ȭ�� ���ͷ� ��ȯ
            Vector3 movementDirection = new Vector3(axis.x, axis.y).normalized;

            // ������ �࿡ �ӵ��� ����
            float hSpeed = movementDirection.x * playerSpeed;
            float vSpeed = movementDirection.y * playerSpeed;

            playerRB.velocity = new Vector3(hSpeed, vSpeed);
        }
    }
}
