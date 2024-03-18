using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float monsterHp;
    public float monsterDmg;
    public float monsterSpeed;

    public GameObject[] drops;
    public float[] dropRate; // 드롭 확률 각 수치의 합은 1

    public MonsterSpawner spawner;

    public bool isIce;
    public bool isPoison;
    private bool isIceCooldown = false;
    private bool isPoisonCooldown = false;

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
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);

            DropItem();
            Destroy(gameObject);
        }

        FindTarget();

        if (isPoison && !isPoisonCooldown)
        {
            isPoisonCooldown = true;
            StartCoroutine(PoisonEffect());
            isPoison = false;
        }

        nextDamageTime += Time.deltaTime;

        if (isPlayer && nextDamageTime > monsterAttackSpeed)
        {
            GameManager.instance.player.playerHp -= monsterDmg;
            nextDamageTime = 0f;
            isPlayer = false;

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Melee);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        float damage = 0; // 기본 데미지는 0으로 설정

        if (collision.CompareTag("Bullet"))
        {
            damage = GameManager.instance.attackDmg;
            TextOutput(damage, 0);

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);

            if (GameManager.instance.poison >= 3)
            {
                isPoison = true;
            }
            if (GameManager.instance.ice >= 3)
            {
                isIce = true;

                float iceCoolTime;
                iceCoolTime = GameManager.instance.ice > 5 ? 1 : GameManager.instance.ice > 3 ? 0.5f : 0.25f;

                StartCoroutine(IceCooldown(iceCoolTime)); // 얼음 효과 쿨다운 시작
            }
        }
        else if (collision.CompareTag("FireBoom"))
        {
            damage = (int)Mathf.Round(GameManager.instance.attackDmg / 2f); // 반올림해서 계산
            TextOutput(damage, 1);

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private IEnumerator PoisonEffect()
    {
        int duration = 5; // 독 효과 지속 시간 (초)

        float poisonInterval;
        poisonInterval = GameManager.instance.poison > 5 ? 0.5f : GameManager.instance.poison > 3 ? 0.75f : 1;

        for (int i = 0; i < duration; i++)
        {
            int poisonDmg = (int)Mathf.Round(GameManager.instance.attackDmg / 3f);
            TextOutput(poisonDmg, 2);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
            yield return new WaitForSeconds(poisonInterval);
        }
        isPoisonCooldown = false;
    }

    private IEnumerator IceCooldown(float duration)
    {
        isIceCooldown = true; // 쿨다운 활성화

        yield return new WaitForSeconds(duration); // 지정된 시간 동안 대기

        isIceCooldown = false; // 쿨다운 비활성화
    }

    private void FindTarget()
    {
        if (target != null)
        {
            targetDir = (target.transform.position - transform.position).normalized;

            float monHSpeed = targetDir.x * monsterSpeed;
            float monVSpeed = targetDir.y * monsterSpeed;

            // 얼음 효과가 활성화되어 있고 쿨다운 중이 아닌 경우에만 이동
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


    public void TextOutput(float damage, int index)
    {
        monsterHp -= damage;
        GameObject dmgTxt = Instantiate(dmgText, transform.position, Quaternion.identity);
        dmgTxt.GetComponent<DamageText>().damage = damage;
        dmgTxt.GetComponent<DamageText>().index = index;
    }
}
