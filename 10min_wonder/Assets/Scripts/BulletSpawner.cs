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
            GameObject closestMonster = MonsterUtils.FindClosestMonster(transform, 0);
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
        GameObject closestMonster = MonsterUtils.FindClosestMonster(transform, 0);
        if (closestMonster != null)
        {
            bullet.GetComponent<Bullet>().SetTarget(closestMonster);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
            isRange = true;
    }
}
