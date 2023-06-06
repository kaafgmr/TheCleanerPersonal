using UnityEngine;
using UnityEngine.Events;

public class FinishGameChecker : MonoBehaviour
{
    public UnityEvent onGameFinished;

    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetInt("Win", 1);
        onGameFinished.Invoke();
    }
}