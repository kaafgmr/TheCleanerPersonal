using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string nameController;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(nameController, Mathf.Log10(volume) * 20);
    }
}
