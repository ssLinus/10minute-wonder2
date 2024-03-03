using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float monsterHp;
    public float monsterDmg;
    public float monsterSpeed;

    private Rigidbody2D monsterRB;

    private GameObject target;
    private Vector2 targetDir;

    private float monsterAttackSpeed = 1f;
    private float nextDamageTime = 0f;
    private bool isPlayer;

    void Start()
    {
        isPlayer = false;

        monsterRB = this.GetComponent<Rigidbody2D>(); //Rigidbody2D √ ±‚»≠
    }

    void FixedUpdate()
    {
        if (monsterHp <= 0)
        {
            Destroy(gameObject);
        }

        FindTarget();

        nextDamageTime += Time.deltaTime;

        if (isPlayer && nextDamageTime > monsterAttackSpeed)
        {
            GameManager.instance.player.playerHp -= monsterDmg;

            nextDamageTime = 0f;
        }
    }

    private void FindTarget()
    {
        if (target == null)
        {
            target = GameManager.instance.player.GetComponent<GameObject>();
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

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }
}
