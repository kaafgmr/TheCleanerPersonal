using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Light DebugDirectionalLight;
    public GameObject player;
    public GameObject playerFoot;
    public FlashlightBehaviour flashlight;
    public Transform ScreamerPoint;
    public string[] ScenesToLoadOnStart;
    public UnityEvent InitGame;
    public UnityEvent OnAllScenesLoaded;
    [HideInInspector]public Vector3 playerPos { get => playerFoot.transform.position; }
    
    private Transform playerTransform;
    private float playerFOV;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

#if !UNITY_EDITOR
        DebugDirectionalLight.enabled = false;
#endif

        playerTransform = player.transform;
        playerFOV = player.GetComponent<Camera>().fieldOfView;
    }


    private void Start()
    {
        for (int i = 0; i < ScenesToLoadOnStart.Length; i++)
        {
            MenuControl.instance.LoadScene(ScenesToLoadOnStart[i], UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
        playerFoot = GameObject.Find("OVRCameraRig");

        InitGame.Invoke();
        InitOculusSettings();
    }

    void InitOculusSettings()
    {
        OVRManager.foveatedRenderingLevel = OVRManager.FoveatedRenderingLevel.High;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public float GetPlayerFOV()
    {
        return playerFOV;
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }
}
