using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float monsterHp;
    public float monsterDmg;
    public float monsterSpeed;

    public GameObject[] drops;
    public float[] dropRate; // ��� Ȯ�� �� ��ġ�� ���� 1

    public MonsterSpawner spawner;

    public bool isIce;
    public bool isPoison;
    private bool isIceCooldown = false;

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

        monsterRB = this.GetComponent<Rigidbody2D>(); //Rigidbody2D �ʱ�ȭ
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        float damage = 0; // �⺻ �������� 0���� ����

        if (collision.CompareTag("Bullet"))
        {
            damage = GameManager.instance.attackDmg;
            TextOutput(damage, 0);

            if (GameManager.instance.poison >= 3)
            {
                isPoison = true;
            }
            if (GameManager.instance.ice >= 3)
            {
                isIce = true;
                StartCoroutine(IceCooldown(1f)); // ���� ȿ�� ��ٿ� ����
            }
        }
        else if (collision.CompareTag("FireBoom"))
        {
            damage = Mathf.Round(GameManager.instance.attackDmg / 2f); // �ݿø��ؼ� ���
            TextOutput(damage, 1);
        }

    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private IEnumerator IceCooldown(float duration)
    {
        isIceCooldown = true; // ��ٿ� Ȱ��ȭ

        yield return new WaitForSeconds(duration); // ������ �ð� ���� ���

        isIceCooldown = false; // ��ٿ� ��Ȱ��ȭ
    }

    private void FindTarget()
    {
        if (target != null)
        {
            targetDir = (target.transform.position - transform.position).normalized;

            float monHSpeed = targetDir.x * monsterSpeed;
            float monVSpeed = targetDir.y * monsterSpeed;

            // ���� ȿ���� Ȱ��ȭ�Ǿ� �ְ� ��ٿ� ���� �ƴ� ��쿡�� �̵�
            if (!isIceCooldown)
                monsterRB.velocity = new Vector2(monHSpeed, monVSpeed);
            else
                monsterRB.velocity = new Vector2(0, 0);
        }
    }

    private void DropItem()
    {
        if (spawner != null)
        {
            float rand = Random.Range(0f, 1f);
            float cumulative = 0f;  // ���� Ȯ���� ����ϴ� ����
            int selectedIndex = 0;

            for (; selectedIndex < dropRate.Length; selectedIndex++)
            {
                cumulative += dropRate[selectedIndex];
                if (rand < cumulative)
                {
                    break;
                }
            }

            // ���õ� �������� �����մϴ�.
            Instantiate(drops[selectedIndex], transform.position, Quaternion.identity);

            spawner.RemoveMonster(this);
        }
    }


    public void TextOutput(float damage, int index)
    {
        monsterHp -= damage;
        GameObject dmgTxt = Instantiate(dmgText, transform.position, Quaternion.identity);
        dmgTxt.GetComponent<DamageText>().damage = damage;
        dmgTxt.GetComponent<DamageText>().index = index;
    }
}
