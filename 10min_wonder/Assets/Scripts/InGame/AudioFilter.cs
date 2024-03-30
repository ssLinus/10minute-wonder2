using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFilter : MonoBehaviour
{
    public AudioHighPassFilter audioHighPass;

    void Start()
    {
        audioHighPass = GetComponent<AudioHighPassFilter>();
        AudioManager.instance.bgmFilter = audioHighPass;
    }
}
