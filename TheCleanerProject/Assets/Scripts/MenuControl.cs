using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public static MenuControl instance;
    public GameObject loadScreenPanel;
    public GameObject mainMenuPanel;
    public TextMeshProUGUI progressPercentage;
    public Slider progressBar;

    public UnityEvent OnSceneLoaded;

    private void Awake()
    {
        instance = this;
        if(loadScreenPanel != null)
        {
            loadScreenPanel.SetActive(false);
        }
    }

    public void LoadScene(string name, LoadSceneMode loadMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(name, loadMode);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void LoadLevel(int levelNumber, LoadSceneMode loadMode = LoadSceneMode.Single)
    {
        LoadScene("Level" + levelNumber, loadMode);
    }

    public void LoadWithProgressBar(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string SceneName)
    {
        AsyncOperation loader = SceneManager.LoadSceneAsync(SceneName);

        if (loadScreenPanel != null && mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
            loadScreenPanel.SetActive(true);
        }
        
        while(!loader.isDone)
        {
            //change the value so it goes from 0 to 1 instead of 0 to 0.9f
            float progress = Mathf.Clamp01(loader.progress / 0.9f);
            if (progressBar != null) { progressBar.value = progress; }
            //change the value so it goes from 0 to 100 with 2 decimal places
            progress = Mathf.Round(progress * 10000) / 100;
            if (progressPercentage != null) { progressPercentage.text = progress + "%"; }
            yield return null;
        }
        OnSceneLoaded.Invoke();
    }

    public int GetSceneCount()
    {
        return SceneManager.sceneCount;
    }

    public Scene GetLoadedSceneByID(int index)
    {
        return SceneManager.GetSceneAt(index);
    }

    public bool IsAreadyLoaded(Scene scene)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == scene.name)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsAreadyLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    public void UnloadSceneASYNC(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }

    public void UnloadLevel(int number)
    {
        UnloadSceneASYNC("Level" + number);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
