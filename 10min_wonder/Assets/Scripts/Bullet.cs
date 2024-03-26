using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    private GameObject targetMonster;

    private float bulletSpeed; // 인스펙터창에서 조정 (초기값 : 10)
    private float bulletLifeTime; // (초기값 : 2)
    private float bulletPen; // 총알 관통 (초기값 : 0)

    public int fire;
    public int electric;

    public int bounce;

    public GameObject fireBoom;

    public void SetTarget(GameObject target)
    {
        targetMonster = target;
    }

    public void Start()
    {
        bulletSpeed = GameManager.Instance.bulletSpeed;
        bulletLifeTime = GameManager.Instance.bulletLifeTime;
        bulletPen = GameManager.Instance.bulletPen;
        fire = GameManager.Instance.fire;
        electric = GameManager.Instance.electric;

        bounce = electric < 3 ? 0 : electric < 5 ? 1 : electric < 7 ? 2 : 3;

        bulletRB = GetComponent<Rigidbody2D>();

        if (bulletRB != null && targetMonster != null)
        {
            DirectionToTarget(targetMonster);
        }

        Destroy(gameObject, bulletLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster"))
        { return; }

        if (fire >= 3)
        {
            Instantiate(fireBoom, transform.position, Quaternion.identity);
        }

        if (bounce > 0 && bulletPen > 0)
        {
            GameObject closestMonster = MonsterUtils.FindClosestMonster(transform, 1);
            if (closestMonster != null && closestMonster != collision.gameObject)
            {
                DirectionToTarget(closestMonster);
            }
            bounce--;
        }

        bulletPen--;

        if (bulletPen < 0)
        {
            Destroy(gameObject);
        }
    }

    public void DirectionToTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float hSpeed = direction.x * bulletSpeed;
        float vSpeed = direction.y * bulletSpeed;
        bulletRB.velocity = new Vector3(hSpeed, vSpeed);

        // 몬스터를 향하도록 총알 회전
        Vector3 directionToMonster = target.transform.position - transform.position;
        float angle = Mathf.Atan2(directionToMonster.y, directionToMonster.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
