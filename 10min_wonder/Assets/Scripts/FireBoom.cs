using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoom : MonoBehaviour
{
    public float targetSize; // 목표하는 크기
    private Vector3 boomSize;
    private Vector3 initialSize;

    public float duration; // 크기를 조절하는 데 걸리는 시간 0.2
    private float startTime;

    void Start()
    {
        int fire = GameManager.instance.fire;
        targetSize = fire < 5 ? 10 : fire < 7 ? 15 : 20;
        boomSize = new Vector3(targetSize, targetSize, targetSize);
        startTime = Time.time;
        initialSize = transform.localScale;
    }

    void Update()
    {
        float progress = (Time.time - startTime) / duration;
        transform.localScale = Vector3.Lerp(initialSize, boomSize, progress);

        if (progress >= 1f)
        {
            Destroy(gameObject); // 목표 크기에 도달하면 오브젝트 파괴
        }
    }
}