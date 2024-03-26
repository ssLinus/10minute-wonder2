using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Reposition : MonoBehaviour
{
    private Tilemap tilemap;
    private Color color;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // 초기 색상
        color = Color.white;
    }

    private void Update()
    {
        // 10분 동안 빨간색으로 변하도록 함.
        float timeRatio = Mathf.Clamp01(Time.timeSinceLevelLoad / GameManager.Instance.setTime);

        // 색상 변화 (흰색에서 빨간색으로)
        color = Color.Lerp(Color.white, Color.red, timeRatio);

        // 타일맵의 모든 타일에 색상 적용
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemap.HasTile(localPlace))
            {
                tilemap.SetColor(localPlace, color);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.Instance.player.axis;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 80);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 80);
                }
                break;
        }
    }
}
