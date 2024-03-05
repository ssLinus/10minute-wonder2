using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float monsterHp;
    public float monsterDmg;
    public float monsterSpeed;

    public GameObject[] drops;
    public float[] dropRate; // 드롭 확률 각 수치의 합은 1

    public MonsterSpawner spawner;

    public GameObject dmgText;

    private Rigidbody2D monsterRB;

    private GameObject target;
    private Vector2 targetDir;

    private float monsterAttackSpeed = 1f;
    private float nextDamageTime = 0f;
    private bool isPlayer;

    void Start()
    {
        isPlayer = false;

        target = GameObject.FindGameObjectWithTag("Player");

        monsterRB = this.GetComponent<Rigidbody2D>(); //Rigidbody2D 초기화
    }

    void FixedUpdate()
    {
        if (monsterHp <= 0)
        {
            DropItem();

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
        if (target != null)
        {
            targetDir = (target.transform.position - transform.position).normalized;

            float monHSpeed = targetDir.x * monsterSpeed;
            float monVSpeed = targetDir.y * monsterSpeed;

            monsterRB.velocity = new Vector2(monHSpeed, monVSpeed);
        }
    }

    private void DropItem()
    {
        if (spawner != null)
        {
            float rand = Random.Range(0f, 1f);
            float cumulative = 0f;  // 누적 확률을 계산하는 변수
            int selectedIndex = 0;

            for (; selectedIndex < dropRate.Length; selectedIndex++)
            {
                cumulative += dropRate[selectedIndex];
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            monsterHp -= GameManager.instance.attackDmg;
            GameObject dmgTxt = Instantiate(dmgText, transform.position, Quaternion.identity);
            dmgTxt.GetComponent<DamageText>().damage = GameManager.instance.attackDmg;
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
