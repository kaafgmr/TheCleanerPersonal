using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PrefsManager : MonoBehaviour
{
    
    public Slider Master;
    public Slider Music;
    public Slider SFX;
    public Toggle FullScreenFX;

    // Start is called before the first frame update

    public void Awake()
    {
        WriteSaves();
    }
    public void SaveOptions()
    {
        
        PlayerPrefs.SetFloat("Master", Master.value);
        PlayerPrefs.SetFloat("Music", Music.value);
        PlayerPrefs.SetFloat("SFX", SFX.value);
        PlayerPrefs.SetInt("FullScreenFX", FullScreenFX.isOn ? 1 : 0);



        PlayerPrefs.Save();
    }
    public void WriteSaves()
    {
        Master.value = PlayerPrefs.GetFloat("Master");
        Music.value = PlayerPrefs.GetFloat("Music");
        SFX.value = PlayerPrefs.GetFloat("SFX");
        FullScreenFX.isOn = PlayerPrefs.GetInt("FullScreenFX")==1? true : false;

    }
}
