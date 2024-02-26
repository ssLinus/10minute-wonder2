using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Vector3 axis;

    public float playerSpeed; //인스펙터에서 조정 (초기값 : 5)

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerRB != null)
        {
            axis.x = Input.GetAxis("Horizontal");
            axis.y = Input.GetAxis("Vertical");

            float hSpeed = axis.x * playerSpeed;
            float vSpeed = axis.y * playerSpeed;

            playerRB.velocity = new Vector3(hSpeed, vSpeed);
        }
    }
}
