using UnityEngine;

public class DynamicSceneLoader : MonoBehaviour
{
    [Header("Loading")]
    public string[] scenesToLoad;

    [Header("Unloading")]
    [Header("*Will be reloaded by going backwards*")]
    public string[] scenesToUnload;
    [Header("*Optional*")]
    [Header("Scenes to ensure unload")]
    public string[] scenesToUnloadEnsured;


    private Vector3 enterPos;

    private void OnTriggerEnter(Collider other)
    {
        enterPos = other.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        Vector3 exitPos = other.transform.position;
        Vector3 playerDir = (exitPos - enterPos).normalized;
        bool forward = Vector3.Dot(transform.forward, playerDir) > 0;

        if (forward)
        {
            LoadScenes(scenesToLoad);
            UnloadScenes(scenesToUnload);
            UnloadScenes(scenesToUnloadEnsured);
        }
        else
        {
            LoadScenes(scenesToUnload);
            UnloadScenes(scenesToLoad);
        }
    }

    void LoadScenes(string[] scenes)
    {
        if (scenes.Length <= 0) return;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (MenuControl.instance.IsAreadyLoaded(scenes[i])) continue;
            
            MenuControl.instance.LoadScene(scenes[i], UnityEngine.SceneManagement.LoadSceneMode.Additive);    
        }

        CalculateBounds.instance?.Recalculate();
    }

    void UnloadScenes(string[] scenes)
    {
        if (scenes.Length <= 0) return;

        for (int i = 0; i < scenes.Length; i++)
        {
            if (!MenuControl.instance.IsAreadyLoaded(scenes[i])) continue;

            MenuControl.instance.UnloadSceneASYNC(scenes[i]);
        }

        CalculateBounds.instance?.Recalculate();
    }
}