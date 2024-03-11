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

    // 게임 시작부터 각 시간대의 끝 시간(분 단위)을 저장하는 배열
    float[] timeThresholds = { 1, 3, 5, 7, 9, 10 };

    // 각 시간대에 생성할 몬스터의 인덱스들을 저장하는 배열
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

        float minutes = Time.time / 60.0f;  // 게임 시작부터 현재까지의 시간(분 단위)

        // 현재 시간대를 찾습니다.
        int timeIndex = 0;

        for (; timeIndex < timeThresholds.Length; timeIndex++)
        {
            if (minutes < timeThresholds[timeIndex])
                break;
        }

        // 현재 시간대에 맞는 몬스터 인덱스를 랜덤하게 선택합니다.
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
