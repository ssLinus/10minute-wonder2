using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {

    }

    private void Update()
    {
        masterValue.text = masterVolume.value.ToString();
        bgmValue.text = bgmVolume.value.ToString();
        effectValue.text = effectVolume.value.ToString();
    }

    public void MasterMute(bool isOn)
    {
        if (isOn)
        {
        }
        else
        {
        }
    }

    public void BgmMute(bool isOn)
    {
        if (isOn)
        {
        }
        else
        {
        }
    }

    public void EffectMute(bool isOn)
    {
        if (isOn)
        {
        }
        else
        {
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