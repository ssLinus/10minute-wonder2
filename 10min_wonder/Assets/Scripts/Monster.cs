using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float monsterHp;
    public float monsterDmg;
    public float monsterSpeed;

    public GameObject[] drops;

    public MonsterSpawner spawner;

    private Rigidbody2D monsterRB;

    private GameObject target;
    private Vector2 targetDir;

    private float monsterAttackSpeed = 1f;
    private float nextDamageTime = 0f;
    private bool isPlayer;

    void Start()
    {
        isPlayer = false;

        monsterRB = this.GetComponent<Rigidbody2D>(); //Rigidbody2D 초기화
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
            isPlayer = false;
        }
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

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            // 확률 구간을 정의합니다.
            float[] probabilities = { 0.5f, 0.25f, 0.15f, 0.05f, 0.03f, 0.02f };

            float rand = Random.Range(0f, 1f);
            float cumulative = 0f;  // 누적 확률을 계산하는 변수
            int selectedIndex = 0;

            for (; selectedIndex < probabilities.Length; selectedIndex++)
            {
                cumulative += probabilities[selectedIndex];
                if (rand < cumulative)
                {
                    break;
                }
            }

            // 선택된 아이템을 생성합니다.
            Instantiate(drops[selectedIndex], transform.position, Quaternion.identity);

            spawner.RemoveMonster(this);
        }
    }

}
