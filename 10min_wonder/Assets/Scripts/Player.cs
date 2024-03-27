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

    public int playerLevel;

    public float playerExp;
    public float playerMaxExp = 10;

    public int currentWave;

    public Rigidbody2D playerRB;
    public Vector2 axis;

    public VariableJoystick joystick;
    public GameObject joy;

    public Vector2 joyAxis;
    public bool isJoy = false;

    public Looting looting;
    public BulletSpawner bulletSpawner;

    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Start()
    {
        playerMaxHp = GameManager.instance.playerMaxHp;
        playerSpeed = GameManager.instance.playerSpeed;
        currentWave = GameManager.instance.startWave;

        playerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        playerExp = 0;
        playerHp = playerMaxHp;

        if (isJoy) joy.SetActive(true);
        else joy.SetActive(false);

        looting = GetComponentInChildren<Looting>();
        bulletSpawner = GetComponentInChildren<BulletSpawner>();
    }

    private void Update()
    {
        if (playerHp <= 0)
        {
            GameOver();
        }

        // ·¹º§¾÷
        if (playerExp >= playerMaxExp)
        {
            playerLevel++;
            playerExp = (playerExp - playerMaxExp);
            playerMaxExp = playerMaxExp * 1.4f;
            onPlayerLevelUp?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (isJoy)
        {
            float x = joystick.Horizontal;
            float y = joystick.Vertical;

            axis = new Vector2(x, y) * playerSpeed * Time.deltaTime;
            playerRB.MovePosition(playerRB.position + axis);
        }
        else
        {
            axis.x = Input.GetAxisRaw("Horizontal");
            axis.y = Input.GetAxisRaw("Vertical");

            Vector3 movementDirection = new Vector3(axis.x, axis.y).normalized;
            float hSpeed = movementDirection.x * playerSpeed;
            float vSpeed = movementDirection.y * playerSpeed;
            playerRB.velocity = new Vector3(hSpeed, vSpeed);
        }
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", axis.magnitude);

        if (axis.x != 0)
        {
            spriteRenderer.flipX = axis.x < 0;
        }
    }

    public void GameOver()
    {
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
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