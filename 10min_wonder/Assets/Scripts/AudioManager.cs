using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    public AudioSource bgmPlayer;
    public AudioHighPassFilter bgmFilter;

    [Header("SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    public AudioSource[] sfxPlayers;
    int channelIndex;
    public bool sfxMute = false;

    // PlayerPrefs keys for volume settings
    public float BGM_VOLUME_KEY;
    public float SFX_VOLUME_KEY;

    void Awake()
    {
        // Singleton Pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("ÆÄ±«");
        }
        Init();
    }

    void Init()
    {
        // Load volume settings from PlayerPrefs
        if (!PlayerPrefs.HasKey("BGM_VOLUME") && !PlayerPrefs.HasKey("SFX_VOLUME"))
        {
            PlayerPrefs.SetFloat("BGM_VOLUME", BGM_VOLUME_KEY);
            PlayerPrefs.SetFloat("SFX_VOLUME", SFX_VOLUME_KEY);
        }

        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume * (PlayerPrefs.GetFloat("BGM_VOLUME") / 100f);
        bgmPlayer.clip = bgmClip;
        bgmFilter = Camera.main.GetComponent<AudioHighPassFilter>();

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].bypassListenerEffects = true;
            sfxPlayers[i].volume = sfxVolume * (PlayerPrefs.GetFloat("SFX_VOLUME") / 100f);
        }
    }

    public void SetBgmVolume(float volume)
    {
        bgmPlayer.volume = (volume / 100f) * bgmVolume;
        PlayerPrefs.SetFloat("BGM_VOLUME", volume);
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float volume)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].volume = (volume / 100f) * sfxVolume;
        }
        PlayerPrefs.SetFloat("SFX_VOLUME", volume);
        PlayerPrefs.Save();
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmFilter.enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx)
    {
        if (!sfxMute)
        {
            for (int i = 0; i < sfxPlayers.Length; i++)
            {
                int loopIndex = (i + channelIndex) % sfxPlayers.Length;

                if (sfxPlayers[loopIndex].isPlaying)
                    continue;

                int randomIndex = 0;
                if (sfx == Sfx.Hit || sfx == Sfx.Melee)
                {
                    randomIndex = Random.Range(0, 2);
                }

                channelIndex = loopIndex;
                sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
                sfxPlayers[loopIndex].Play();
                break;
            }
        }
    }

    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win }
}
