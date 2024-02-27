using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    private GameObject closestMonster;

    private float bulletSpeed; // �ν�����â���� ���� (�ʱⰪ : 10)
    private float bulletLifeTime; // (�ʱⰪ : 2)
    private float bulletPen; // �Ѿ� ���� (�ʱⰪ : 0)

    public void Start()
    {
        bulletSpeed = GameManager.instance.bulletSpeed;
        bulletLifeTime = GameManager.instance.bulletLifeTime;
        bulletPen = GameManager.instance.bulletPen;

        bulletRB = GetComponent<Rigidbody2D>();

        closestMonster = FindClosestMonster();

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

        Destroy(gameObject, bulletLifeTime);
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

        bulletPen--;

        if (bulletPen < 0)
        {
            Destroy(gameObject);
        }
    }
}
