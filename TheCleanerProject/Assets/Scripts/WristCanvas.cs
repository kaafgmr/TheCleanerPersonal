using UnityEngine;

public class WristCanvas : MonoBehaviour
{
    [SerializeField] GameObject[] rayInteractors;
    private Canvas _wristCanvas;
    private void Start()
    {
        _wristCanvas = GetComponent<Canvas>();
        _wristCanvas.enabled = false;

        for (int i = 0; i < rayInteractors.Length; i++)
        {
            rayInteractors[i].SetActive(false);
        }
    }

    //private void FixedUpdate()
    //{
    //    if (OVRInput.GetDown(OVRInput.Button.Start))
    //    {
    //        ToggleMenu();
    //        ToggleInteractors();
    //    }
    //}

    private void OnDestroy()
    {
        for (int i = 0; i < rayInteractors.Length; i++)
        {
            rayInteractors[i].SetActive(false);
        }
    }

    public void ToggleMenu()
    {
        _wristCanvas.enabled = !_wristCanvas.enabled;
    }

    public void ToggleInteractors()
    {
        for (int i = 0; i < rayInteractors.Length; i++)
        {
            rayInteractors[i].SetActive(_wristCanvas.enabled);
        }
    }
}
