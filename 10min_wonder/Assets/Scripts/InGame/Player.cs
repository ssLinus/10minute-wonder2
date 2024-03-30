using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void OnPlayerDataChanged();
    public static event OnPlayerDataChanged onPlayerLevelUp;
    public static event OnPlayerDataChanged onPlayerWaveUp;

    public float playerMaxHp;
    public float playerSpeed;
    public float playerHp;
    public bool isLive;
    public bool isGameClear;

    public int playerLevel;

    public float playerExp;
    public float playerMaxExp = 10;

    public int currentWave = 0;

    public Rigidbody2D playerRB;
    public Vector2 axis;

    public FixedJoystick fixedJoystick;
    public FloatingJoystick floatingJoystick;
    public DynamicJoystick dynamicJoystick;
    public GameObject[] joys;

    public Vector2 joyAxis;

    public Looting looting;
    public BulletSpawner bulletSpawner;

    SpriteRenderer spriteRenderer;
    Animator anim;

    public void Awake()
    {
        GameManager.instance.player = this;
    }

    public void Start()
    {
        playerMaxHp = GameManager.instance.playerMaxHp;
        playerHp = GameManager.instance.playerMaxHp;
        playerSpeed = GameManager.instance.playerSpeed;

        playerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        playerExp = 0;
        isLive = true;
        isGameClear = false;

        ActiveJoystick();

        looting = GetComponentInChildren<Looting>();
        bulletSpawner = GetComponentInChildren<BulletSpawner>();
    }

    public void Update()
    {
        if (playerHp <= 0 && isLive && !isGameClear)
        {
            isLive = false;
            playerRB.simulated = false;
            anim.SetBool("Dead", true);
        }

        // ·¹º§¾÷
        if (playerExp >= playerMaxExp)
        {
            playerLevel++;
            playerExp = (playerExp - playerMaxExp);
            playerMaxExp = playerMaxExp * 1.3f;
            onPlayerLevelUp?.Invoke();
        }
    }

    public void FixedUpdate()
    {
        if (GameManager.instance.isJoy)
        {
            float x = 0;
            float y = 0;
            if (GameManager.instance.fixedJoy)
            {
                x = fixedJoystick.Horizontal;
                y = fixedJoystick.Vertical;
            }
            else if (GameManager.instance.floatingJoy)
            {
                x = floatingJoystick.Horizontal;
                y = floatingJoystick.Vertical;
            }
            else if (GameManager.instance.dynamicJoy)
            {
                x = dynamicJoystick.Horizontal;
                y = dynamicJoystick.Vertical;
            }

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

    public void LateUpdate()
    {
        anim.SetFloat("Speed", axis.magnitude);

        if (axis.x != 0)
        {
            spriteRenderer.flipX = axis.x < 0;
        }
    }

    public void ActiveJoystick()
    {
        if (GameManager.instance.isJoy)
        {
            joys[0].SetActive(GameManager.instance.fixedJoy);
            joys[1].SetActive(GameManager.instance.floatingJoy);
            joys[2].SetActive(GameManager.instance.dynamicJoy);
        }
        else
        {
            foreach (var joy in joys)
            {
                joy.SetActive(false);
            }
        }
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