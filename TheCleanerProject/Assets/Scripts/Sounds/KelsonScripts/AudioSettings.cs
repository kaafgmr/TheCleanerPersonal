using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AudioVolumeSetter
{
    public string name;
    public Slider slider;
}

public class AudioSettings : MonoBehaviour
{
    public List<AudioVolumeSetter> audios;

    private void Awake()
    {
        LoadAudioSettings();
    }

    public void SaveAudioSettings()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            SetAudioTo(audios[i].name, audios[i].slider.value);
        }

        PlayerPrefs.Save();
    }

    public void LoadAudioSettings()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            audios[i].slider.value = getValueOf(audios[i].name);
        }
    }

    public void SetAudioTo(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }

    public float getValueOf(string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return PlayerPrefs.GetFloat(name);
        }

        return 0.001f;
    }
}