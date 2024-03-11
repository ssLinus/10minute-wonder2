using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Monster[] monsterPrefabs;
    public Transform playerTransform;

    public int spawnCount;
    public int wave;

    public float spawnMinDistance;
    public float spawnMaxDistance;

    private float spawnDistance;

    private List<Monster> monsters = new List<Monster>();


    void Start()
    {
        playerTransform = GameManager.instance.player.transform;
    }

    void Update()
    {
        if (monsters.Count <= 0)
        {
            SpawnerWave();
        }
    }

    private void SpawnerWave()
    {
        GameManager.instance.player.currentWave++;
        GameManager.instance.player.WaveUp();

        wave++;

        spawnCount = Mathf.RoundToInt(wave * 1.3f);

        for (int i = 0; i < spawnCount; i++)
        {
            CreateMonster();
        }
    }

    // ���� ���ۺ��� �� �ð����� �� �ð�(�� ����)�� �����ϴ� �迭
    float[] timeThresholds = { 1, 3, 5, 7, 9, 10 };

    // �� �ð��뿡 ������ ������ �ε������� �����ϴ� �迭
    int[][] monsterIndices = {
    new int[] {0},
    new int[] {0, 0, 0, 1},
    new int[] {0, 0, 1, 1, 2},
    new int[] {1, 1, 1, 2, 3},
    new int[] {2, 3, 3, 4},
    new int[] {3, 4, 4}
    };

    private void CreateMonster()
    {
        spawnDistance = Random.Range(spawnMinDistance, spawnMaxDistance);

        Vector3 randomDirection = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPosition = playerTransform.position + randomDirection;

        float minutes = Time.time / 60.0f;  // ���� ���ۺ��� ��������� �ð�(�� ����)

        // ���� �ð��븦 ã���ϴ�.
        int timeIndex = 0;

        for (; timeIndex < timeThresholds.Length; timeIndex++)
        {
            if (minutes < timeThresholds[timeIndex])
                break;
        }

        // ���� �ð��뿡 �´� ���� �ε����� �����ϰ� �����մϴ�.
        int[] indices = monsterIndices[timeIndex];
        int monsterIndex = indices[Random.Range(0, indices.Length)];

        Monster monster = Instantiate(monsterPrefabs[monsterIndex], spawnPosition, Quaternion.identity);

        monster.spawner = this;
        monsters.Add(monster);
    }


    public void RemoveMonster(Monster monster)
    {
        monsters.Remove(monster);
    }
}
