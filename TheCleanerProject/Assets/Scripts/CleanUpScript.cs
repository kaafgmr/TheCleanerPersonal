#if UNITY_EDITOR
using UnityEngine;

[ExecuteInEditMode]
public class CleanUpScript : MonoBehaviour
{
    public GameObject parentToClean;
    public bool Activate = false;

    private void Update()
    {
        if (Activate)
        {
            CleanUP();
        }
    }

    void CleanUP()
    {
        if (Activate == false) return;
        Activate = false;

        BoxCollider[] objects = parentToClean.GetComponentsInChildren<BoxCollider>(true);
        CapsuleCollider[] objects2 = parentToClean.GetComponentsInChildren<CapsuleCollider>(true);

        Debug.Log(objects.Length + objects2.Length);
    }
}
#endif