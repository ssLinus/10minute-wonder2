using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    private GameObject targetMonster;

    private float bulletSpeed; // �ν�����â���� ���� (�ʱⰪ : 10)
    private float bulletLifeTime; // (�ʱⰪ : 2)
    private float bulletPen; // �Ѿ� ���� (�ʱⰪ : 0)

    public int fire;
    public int electric;
    public int ice;
    public int poison;

    public void SetTarget(GameObject target)
    {
        targetMonster = target;
    }

    public void Start()
    {
        bulletSpeed = GameManager.instance.bulletSpeed;
        bulletLifeTime = GameManager.instance.bulletLifeTime;
        bulletPen = GameManager.instance.bulletPen;

        bulletRB = GetComponent<Rigidbody2D>();

        if (bulletRB != null && targetMonster != null)
        {
            // �Ѿ� �ʱ� �ӵ� ����
            Vector2 direction = (targetMonster.transform.position - transform.position).normalized;
            bulletRB.velocity = direction * bulletSpeed;

            // ���͸� ���ϵ��� �Ѿ� ȸ��
            Vector3 directionToMonster = targetMonster.transform.position - transform.position;
            float angle = Mathf.Atan2(directionToMonster.y, directionToMonster.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        Destroy(gameObject, bulletLifeTime);
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
