using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider masterVolume;
    public Text masterValue;
    public Toggle masterMute;
    [Space]
    public Slider bgmVolume;
    public Text bgmValue;
    public Toggle bgmMute;
    [Space]
    public Slider effectVolume;
    public Text effectValue;
    public Toggle effectMute;
    [Space]
    public Toggle vibration;
    public GameObject vibrationOn;
    public GameObject vibrationOff;
    [Space]
    public VariableJoystick variableJoystick;
    public GameObject joystick;
    public Toggle joystickOnOff;
    public GameObject joystickOn;
    public GameObject joystickOff;
    public GameObject joystickGroup;
    public Toggle fixedJoystick;
    public Toggle floatingJoystick;
    public Toggle dynamicJoystick;

    void Start()
    {
        
    }

    void Update()
    {
        masterValue.text = masterVolume.value.ToString();
        bgmValue.text = bgmVolume.value.ToString();
        effectValue.text = effectVolume.value.ToString();
    }

    public void MasterMute(bool isOn)
    {
        if(isOn)
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
        {

        }
        else
        {

        }
    }

    public void FloatingJoystick(bool isOn)
    {
        if (isOn)
        {

        }
        else
        {

        }
    }

    public void DynamicJoystick(bool isOn)
    {
        if (isOn)
        {

        }
        else
        {

        }
    }
}
