using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    private Vector3 axis;

    public float bulletSpeed; // 인스펙터창에서 조정 (초기값 : 10)
    public float bulletLifeTime; // (초기값 : 2)
    public int pen; // 총알 관통 (초기값 : 0)

    private GameObject closestMonster;

    private void OnEnable()
    {
        closestMonster = FindClosestMonster();

        bulletRB = GetComponent<Rigidbody2D>();

        if (bulletRB != null)
        {
            // 총알 초기 속도 설정
            Vector2 direction = (closestMonster.transform.position - transform.position).normalized;
            bulletRB.velocity = direction * bulletSpeed;
        }

        if (closestMonster != null)
        {
            // 몬스터를 향하도록 총알 회전
            Vector3 direction = closestMonster.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        Invoke("BulletEnd", bulletLifeTime);
    }

    void Start()
    {

    }

    // 가장 가까운 몬스터를 찾는 함수
    GameObject FindClosestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // "monster" 태그를 가진 모든 몬스터를 배열로 가져옴

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster"))
        { return; }

        pen--;

        if (pen < 0)
        {
            gameObject.SetActive(false);
        }
    }

    void BulletEnd()
    {
        gameObject.SetActive(false);
    }
}
