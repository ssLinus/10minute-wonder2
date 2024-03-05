using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looting : MonoBehaviour
{
    public float lootingRange; // 0.5
    public new CircleCollider2D collider2D;

    private GameObject target;
    private float speed = 10;

    void Start()
    {
        lootingRange = GameManager.instance.lootingRange;

        collider2D = GetComponent<CircleCollider2D>();
        collider2D.radius = lootingRange;

        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Exp"))
        {
            // 플레이어 방향으로 이동
            Vector2 direction = (target.transform.position - other.transform.position).normalized;
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
            }
        }
    }

}
