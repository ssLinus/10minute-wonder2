using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    public Text levelText;
    public Text waveText;
    public Text monsterKillText;
    public Text survivalTimeText;
    public Text scoreText;

    public float level;
    public float wave;
    public float monsterKill;
    public float time;
    public float score;

    private void OnEnable()
    {
        level = GameManager.Instance.player.playerLevel;
        wave = GameManager.Instance.player.currentWave;
        monsterKill = GameManager.Instance.monsterKill;
        time = Time.timeSinceLevelLoad;
        score = (int)(level * 10000 + wave * 10000 + monsterKill * 1000 + (time / GameManager.Instance.setTime) * 1000000);

        int min = (int)(time / 60);
        float sec = (time % 60);

        levelText.text = "Level            " + level.ToString();
        waveText.text = "Wave             " + wave.ToString();
        monsterKillText.text = "Kill             " + monsterKill.ToString();
        survivalTimeText.text = "Survival Time    " + min.ToString("D2") + ":" + sec.ToString("00.00");
        scoreText.text = "Score            " + score.ToString("00000000");
    }
}
