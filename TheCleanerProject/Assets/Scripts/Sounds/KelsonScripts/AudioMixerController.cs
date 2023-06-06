using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string AudioName;

    private void Start()
    {
        if (PlayerPrefs.HasKey($"{AudioName}Volume"))
        {
            audioMixer.SetFloat(AudioName, Mathf.Log10(PlayerPrefs.GetFloat($"{AudioName}Volume")) * 20);
        }
        else
        {
            audioMixer.SetFloat(AudioName, Mathf.Log10(0.001f) * 20);
        }
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(AudioName, Mathf.Log10(value) * 20);
    }
}
