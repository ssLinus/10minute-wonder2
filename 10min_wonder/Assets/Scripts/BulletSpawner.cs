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

        if (timeAfterSpawn > (1f / attackSpeed))
        {
            timeAfterSpawn = 0f;
            Fire();
        }
    }

    void Fire()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
