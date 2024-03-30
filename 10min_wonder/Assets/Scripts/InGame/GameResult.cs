using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    public GameObject gameClear;
    public GameObject gameOver;
    public Text resultText;
    public Text highScoreText;
    public Text rewardCoinText;

    public float level;
    public float wave;
    public float monsterKill;
    public float time;
    public int score;

    public void Start()
    {

        Time.timeScale = 0;
        AudioManager.instance.PlayBgm(false);

        level = GameManager.instance.player.playerLevel;
        wave = GameManager.instance.player.currentWave;
        monsterKill = GameManager.instance.monsterKill;
        time = Time.timeSinceLevelLoad;
        score = (int)(level * 1000 + wave * 1000 + monsterKill * 100 + (time / GameManager.instance.setTime) * 100000);

        if (GameManager.instance.player.isGameClear)
            score += 100000;

        int min = (int)(time / 60);
        float sec = (time % 60);

        resultText.text = level.ToString() + "\n"
            + wave.ToString() + "\n"
            + monsterKill.ToString() + "\n"
            + min.ToString("D2") + ":" + sec.ToString("00.00") + "\n"
            + score.ToString("000000");

        if (score > GameManager.instance.nowPlayer.highScore)
        {
            GameManager.instance.nowPlayer.highScore = (int)score;
        }
        highScoreText.text = score.ToString("000000");

        int rewardCoin = (int)(score / 1000);
        rewardCoinText.text = "+" + rewardCoin;
        GameManager.instance.nowPlayer.coin += rewardCoin;

        if (GameManager.instance.player.isGameClear)
        { 
            gameOver.SetActive(false);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
        }
        else
        { 
            gameClear.SetActive(false);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
        }

        GameManager.instance.SavePlayerData();
    }
}
