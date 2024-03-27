using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public float exp; // 2, 5, 10
    public float multipler;

    public bool isMag;
    private Rigidbody2D expRB;
    private GameObject target;
    private Vector2 targetDir;
    private float speed = 10;

    private void Start()
    {
        isMag = false;

        target = GameObject.FindGameObjectWithTag("Player");
        expRB = this.GetComponent<Rigidbody2D>();

        multipler = GameManager.instance.expMultipler;
    }

    private void Update()
    {
        if (isMag)
        {
            if (target != null)
            {
                targetDir = (target.transform.position - transform.position).normalized;

                float monHSpeed = targetDir.x * speed;
                float monVSpeed = targetDir.y * speed;

                expRB.velocity = new Vector2(monHSpeed, monVSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.player.playerExp += exp * multipler;

            Destroy(gameObject);
        }
    }
}
