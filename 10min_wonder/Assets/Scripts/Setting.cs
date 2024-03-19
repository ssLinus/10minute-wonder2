using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider masterVolume;
    public Text masterValue;

    [Space]
    public Slider bgmVolume;
    public Text bgmValue;

    [Space]
    public Slider effectVolume;
    public Text effectValue;

    [Space]
    public GameObject vibrationOn;
    public GameObject vibrationOff;

    [Space]
    public VariableJoystick variableJoystick;
    public GameObject joystick;
    public GameObject joystickOn;
    public GameObject joystickOff;
    public GameObject joystickGroup;

    private void Awake()
    {

    }

    private void Update()
    {
        masterValue.text = masterVolume.value.ToString();
        bgmValue.text = bgmVolume.value.ToString();
        effectValue.text = effectVolume.value.ToString();

        // Master
        AudioManager.instance.bgmPlayer.volume = 0.2f * (masterVolume.value / 100);
        for (int i = 0; i < AudioManager.instance.sfxPlayers.Length; i++)
        { AudioManager.instance.sfxPlayers[i].volume = 0.5f * (masterVolume.value / 100); }

        // Bgm
        AudioManager.instance.bgmPlayer.volume = 0.2f * (bgmVolume.value / 100) * (masterVolume.value / 100);

        // Sfx
        for (int i = 0; i < AudioManager.instance.sfxPlayers.Length; i++)
        { AudioManager.instance.sfxPlayers[i].volume = 0.5f * (effectVolume.value / 100) * (masterVolume.value / 100); }
    }

    public void MasterMute(bool isOn)
    {
        if (isOn)
        {
            BgmMute(true);
            EffectMute(true);
        }
        else
        {
            BgmMute(false);
            EffectMute(false);
        }
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
    }

    public void Vibration(bool isOn)
    {
        if (isOn)
        {
        }
        else
        {
        }
    }

    public void JoystickOnOff(bool isOn)
    {
        if (isOn)
        {
            joystickOn.SetActive(true);
            joystickOff.SetActive(false);

            joystick.SetActive(true);
            joystickGroup.SetActive(true);

            GameManager.instance.player.isJoy = true;
        }
        else
        {
            joystickOn.SetActive(false);
            joystickOff.SetActive(true);

            joystick.SetActive(false);
            joystickGroup.SetActive(false);

            GameManager.instance.player.isJoy = false;
        }
    }

    public void FixedJoystick(bool isOn)
    {
        if (isOn)
            variableJoystick.SetMode(JoystickType.Fixed);
    }

    public void FloatingJoystick(bool isOn)
    {
        if (isOn)
            variableJoystick.SetMode(JoystickType.Floating);
    }

    public void DynamicJoystick(bool isOn)
    {
        if (isOn)
            variableJoystick.SetMode(JoystickType.Dynamic);
    }
}