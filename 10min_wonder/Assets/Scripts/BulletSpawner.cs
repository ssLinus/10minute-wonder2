using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int initialPoolSize = 10;
    public float attackSpeed = 1f;

    private List<GameObject> bulletPool;

    void Awake()
    {
        // Initialize bullet pool
        bulletPool = new List<GameObject>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

        // Start bullet spawning coroutine
        StartCoroutine(SpawnBullets());
    }

    IEnumerator SpawnBullets()
    {
        while (true)
        {
            // Find inactive bullet in the pool
            GameObject inactiveBullet = bulletPool.Find(bullet => !bullet.activeSelf);

            // If no inactive bullets are found, expand the pool
            if (inactiveBullet == null)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.SetActive(false);
                bulletPool.Add(bullet);
                inactiveBullet = bullet;
            }

            // Activate the inactive bullet and reset its position
            inactiveBullet.SetActive(true);
            inactiveBullet.transform.position = transform.position;

            yield return new WaitForSeconds(1f / attackSpeed);
        }
    }
}
