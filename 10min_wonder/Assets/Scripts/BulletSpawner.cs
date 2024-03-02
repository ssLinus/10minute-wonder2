using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;

    private float attackSpeed;
    private float timeAfterSpawn;

    private void Start()
    {
        attackSpeed = GameManager.instance.attackSpeed;
    }

    public void FixedUpdate()
    {
        timeAfterSpawn += Time.deltaTime;
        GameObject closestMonster = FindClosestMonster();

        if (closestMonster != null && timeAfterSpawn > (1f / attackSpeed))
        {
            timeAfterSpawn = 0f;
            Fire();
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
}
