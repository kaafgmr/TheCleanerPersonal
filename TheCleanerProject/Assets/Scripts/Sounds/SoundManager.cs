using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
[Serializable]
public class AudioInfo
{
    public AudioClip clip;
    public string AudioName;
    public AudioMixerGroup Mixer;
    [Range(0,1)]
    public float Volume;
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;
    public List<AudioInfo> Audios = new List<AudioInfo>();
    private List<AudioSource> AudioManager = new List<AudioSource>();
    private Dictionary<string, AudioInfo> ClipList;

    private void Awake()
    {
        ClipList = new Dictionary<string, AudioInfo>();

        for (int i = 0; i < Audios.Count; i++)
        {
            ClipList.Add(Audios[i].AudioName, Audios[i]);

        }
        for (int i = 0; i < transform.childCount; i++)
        {
            AudioManager.Add(transform.GetChild(i).GetComponent<AudioSource>());
        }
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            PlaySound("BackgroundMusic", true);
        }
        else
        {
            Destroy(this);
        }
       
        
    }

    public void PlaySound(string Name, bool loop)
    {
        if (ClipList.ContainsKey(Name))
        {
            for(int i = 0; i < AudioManager.Count; i++)
            {
                if (AudioManager[i].clip == null || !AudioManager[i].isPlaying)
                {
                    AudioManager[i].clip = ClipList[Name].clip;
                    AudioManager[i].loop = loop;
                    AudioManager[i].volume = ClipList[Name].Volume;
                    AudioManager[i].outputAudioMixerGroup = ClipList[Name].Mixer;
                    AudioManager[i].Play();
                    return;
                }
            }
        }
        return;
    }

    public void ResetSound()
    {
        for (int i = 0; i < AudioManager.Count; i++)
        {
            AudioManager[i].clip = null;
        }
    }

    public void Play(string name, bool loop)
    {
        SoundManager.Instance.PlaySound(name, loop);
    }
}
