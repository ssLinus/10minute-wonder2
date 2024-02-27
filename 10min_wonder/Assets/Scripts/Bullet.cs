using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    private Vector3 axis;

    public float bulletSpeed; // �ν�����â���� ���� (�ʱⰪ : 10)
    public float bulletLifeTime; // (�ʱⰪ : 2)
    public int pen; // �Ѿ� ���� (�ʱⰪ : 0)

    private GameObject closestMonster;

    private void OnEnable()
    {
        closestMonster = FindClosestMonster();

        bulletRB = GetComponent<Rigidbody2D>();

        if (bulletRB != null)
        {
            // �Ѿ� �ʱ� �ӵ� ����
            Vector2 direction = (closestMonster.transform.position - transform.position).normalized;
            bulletRB.velocity = direction * bulletSpeed;
        }

        if (closestMonster != null)
        {
            // ���͸� ���ϵ��� �Ѿ� ȸ��
            Vector3 direction = closestMonster.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        Invoke("BulletEnd", bulletLifeTime);
    }

    void Start()
    {

    }

    // ���� ����� ���͸� ã�� �Լ�
    GameObject FindClosestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); // "monster" �±׸� ���� ��� ���͸� �迭�� ������

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
