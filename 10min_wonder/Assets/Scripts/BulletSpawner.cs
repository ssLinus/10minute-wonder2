using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public new CircleCollider2D collider2D;
    public float attackRange;

    public bool isRange;

    public float attackSpeed;
    public float timeAfterSpawn;

    private void Start()
    {
        attackSpeed = GameManager.instance.attackSpeed;
        attackRange = GameManager.instance.attackRange;

        collider2D = GetComponent<CircleCollider2D>();
        collider2D.radius = attackRange;
    }

    public void FixedUpdate()
    {
        timeAfterSpawn += Time.deltaTime;

        if (isRange)
        {
            GameObject closestMonster = FindClosestMonster();

            if (closestMonster != null && timeAfterSpawn > (1f / attackSpeed))
            {
                timeAfterSpawn = 0f;
                Fire();
            }
            isRange = false;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject closestMonster = FindClosestMonster();
        if (closestMonster != null)
        {
            bullet.GetComponent<Bullet>().SetTarget(closestMonster);
        }
    }

    // 가장 가까운 몬스터를 찾는 함수
    GameObject FindClosestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // "Monster" 태그를 가진 모든 몬스터를 배열로 가져옴
        GameObject closestMonster = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject monster in monsters)
        {
            float distance = Vector2.Distance(transform.position, monster.transform.position);
            if (distance < closestDistance)
            {
                closestMonster = monster;
                closestDistance = distance;
            }
        }

        return closestMonster;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
            isRange = true;
    }
}
