using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void OnPlayerDataChanged();
    public static event OnPlayerDataChanged onPlayerLevelUp;
    public static event OnPlayerDataChanged onPlayerWaveUp;
    public static event OnPlayerDataChanged onGameOver;

    public float playerMaxHp;
    public float playerSpeed;
    public float playerHp;
    [Space]
    public int playerLevel;
    public float playerExp;
    public float playerMaxExp;
    [Space]
    public int currentWave;
    [Space]
    public Rigidbody2D playerRB;
    public Vector3 axis;
    [Space]
    public VariableJoystick joystick;
    public Vector2 joyAxis;
    public bool isJoy = false;

    void Start()
    {
        playerMaxHp = GameManager.instance.playerMaxHp;
        playerSpeed = GameManager.instance.playerSpeed;
        currentWave = GameManager.instance.startWave;
        playerRB = GetComponent<Rigidbody2D>();
        playerHp = playerMaxHp;
    }

    void Update()
    {
        if (playerHp <= 0)
        {
            GameOver();
        }

        if (playerExp >= playerMaxExp)
        {
            playerLevel++;
            playerExp = (playerExp - playerMaxExp);
            playerMaxExp = playerMaxExp * 1.4f;
            onPlayerLevelUp?.Invoke();
        }
    }

    void FixedUpdate()
    {
        if (playerRB != null)
        {
            axis.x = Input.GetAxisRaw("Horizontal");
            axis.y = Input.GetAxisRaw("Vertical");
        }

        Vector3 movementDirection = new Vector3(axis.x, axis.y).normalized;
        float hSpeed = movementDirection.x * playerSpeed;
        float vSpeed = movementDirection.y * playerSpeed;
        playerRB.velocity = new Vector3(hSpeed, vSpeed);

        if (isJoy)
        {
            float x = joystick.Horizontal;
            float y = joystick.Vertical;

            joyAxis = new Vector2(x, y) * playerSpeed * Time.deltaTime;
            playerRB.MovePosition(playerRB.position + joyAxis);
        }
    }

    public void GameOver()
    {
        onGameOver?.Invoke();
    }

    public void LevelUpOpen()
    {
        onPlayerLevelUp?.Invoke();
    }

    public void WaveUp()
    {
        onPlayerWaveUp?.Invoke();
    }
}