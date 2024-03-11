using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoom : MonoBehaviour
{
    public float targetSize; // ��ǥ�ϴ� ũ��
    private Vector3 boomSize;
    private Vector3 initialSize;

    public float duration; // ũ�⸦ �����ϴ� �� �ɸ��� �ð� 0.2
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
            Destroy(gameObject); // ��ǥ ũ�⿡ �����ϸ� ������Ʈ �ı�
        }
    }
}