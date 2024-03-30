using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [Space]
    public Slider bgmVolume;
    public Text bgmValue;

    [Space]
    public Slider effectVolume;
    public Text effectValue;

    [Space]
    public GameObject joystickGroup;
    public Toggle[] joyTypes;

    [Space]
    public GameObject checkQuit;
    public GameObject checkLobby;

    public void OnEnable()
    {
        // Load volume settings from PlayerPrefs and update UI
        bgmVolume.value = PlayerPrefs.GetFloat("BGM_VOLUME");
        effectVolume.value = PlayerPrefs.GetFloat("SFX_VOLUME");
        UpdateVolume();

        JoystickCheck();
        if (GameManager.instance.fixedJoy)
        { joyTypes[0].isOn = true; }
        else if (GameManager.instance.floatingJoy)
        { joyTypes[1].isOn = true; }
        else if (GameManager.instance.dynamicJoy)
        { joyTypes[2].isOn = true; }
    }

    private void UpdateVolume()
    {
        bgmValue.text = bgmVolume.value.ToString();
        effectValue.text = effectVolume.value.ToString();
    }

    public void BgmVolumeChanged()
    {
        AudioManager.instance.SetBgmVolume(bgmVolume.value);
        UpdateVolume();
    }

    public void EffectVolumeChanged()
    {
        AudioManager.instance.SetSfxVolume(effectVolume.value);
        UpdateVolume();
    }

    public void BgmMute(bool isOn)
    {
        if (isOn)
        {
            AudioManager.instance.PlayBgm(false);
        }
        else
        {
            AudioManager.instance.PlayBgm(true);
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void EffectMute(bool isOn)
    {
        if (isOn)
        {
            AudioManager.instance.sfxMute = true;
        }
        else
        {
            AudioManager.instance.sfxMute = false;
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void JoystickCheck()
    {
        if (!GameManager.instance.fixedJoy && !GameManager.instance.floatingJoy && !GameManager.instance.dynamicJoy)
        {
            GameManager.instance.nowPlayer.isJoy = false;
        }
        else
        {
            GameManager.instance.nowPlayer.isJoy = true;
        }
    }

    public void FixedJoystick(bool isOn)
    {
        if (isOn)
        {
            GameManager.instance.nowPlayer.fixedJoy = true;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        }
        else
        {
            GameManager.instance.nowPlayer.fixedJoy = false;
        }
        JoystickCheck();
    }

    public void FloatingJoystick(bool isOn)
    {
        if (isOn)
        {
            GameManager.instance.nowPlayer.floatingJoy = true;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        }
        else
        {
            GameManager.instance.nowPlayer.floatingJoy = false;
        }
        JoystickCheck();
    }

    public void DynamicJoystick(bool isOn)
    {
        if (isOn)
        {
            GameManager.instance.nowPlayer.dynamicJoy = true;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        }
        else
        {
            GameManager.instance.nowPlayer.dynamicJoy = false;
        }
        JoystickCheck();
    }

    public void CheckQuit()
    {
        checkQuit.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameQuit()
    {
        checkQuit.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        Application.Quit();
    }

    public void CloseQuit()
    {
        checkQuit.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }    

    public void CheckLobby()
    {
        checkLobby.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameGiveUp()
    {
        checkLobby.SetActive(false);
        gameObject.SetActive(false);
        if (GameManager.instance.player.isLive)
            GameManager.instance.player.playerHp = 0;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void CloseLobby()
    {
        checkLobby.SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }
}