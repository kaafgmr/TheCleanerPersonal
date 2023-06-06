using System.Collections;
using UnityEngine;

public class BlinkMaterial : MonoBehaviour
{
    public Material material;
    public float fadeSpeed = 1f;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        SetAlpha(0f);
    }

    private void OnTriggerExit(Collider other)
    {
        StopBlink();
    }

    public void StartBlink()
    {
        SetAlpha(0f);
        StartCoroutine(Blink(false));
    }

    public void StopBlink()
    {
        StopCoroutine(Blink(true));
        SetAlpha(0f);
    }

    void SetAlpha(float alpha)
    {
        Color color = material.color;
        color.a = (Mathf.Cos(alpha * 2 * 3.141592f) + 1) / 6;

        material.color = color;
    }

    IEnumerator Blink(bool forceStop)
    {
        SetAlpha((Time.time - startTime) * fadeSpeed);

        yield return new WaitForFixedUpdate();

        if (!forceStop)
        {
            StartCoroutine(Blink(forceStop));
        }
        else
        {
            SetAlpha(0f);
        }
    }
}