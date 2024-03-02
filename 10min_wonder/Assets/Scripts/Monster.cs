using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Rigidbody2D monsterRB;

    public GameObject target;
    public Vector2 targetDir;

    public float monsterHp;
    public float monsterDmg;
    public float monsterSpeed;

    void Start()
    {
        monsterRB = this.GetComponent<Rigidbody2D>(); //Rigidbody2D √ ±‚»≠
        FindTarget();
    }

    void FixedUpdate()
    {
        if (monsterHp <= 0)
        {
            Destroy(gameObject);
        }

        FindTarget();
    }

    private void FindTarget()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        if (target != null)
        {
            targetDir = (target.transform.position - transform.position).normalized;

            float monHSpeed = targetDir.x * monsterSpeed;
            float monVSpeed = targetDir.y * monsterSpeed;

            monsterRB.velocity = new Vector2(monHSpeed, monVSpeed);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            monsterHp -= GameManager.instance.attackDmg;
        }
    }
}
