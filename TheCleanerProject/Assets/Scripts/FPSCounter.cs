using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] float updateDelay = 0.2f;
    [SerializeField] float targetFPS = 72f;

    [HideInInspector] public float currentFPS = 0f;
    float deltaTime = 0f;

    [SerializeField] TextMeshProUGUI textFPS;

    float timer;

    private void Start()
    {
        timer = updateDelay;
    }

    private void Update()
    {
        CalcFPS();

        timer -= Time.unscaledDeltaTime;

        if (timer <= 0)
        {
            if (currentFPS >= targetFPS)
            {
                textFPS.color = Color.green;
            }
            else
            {
                textFPS.color = Color.red;
            }

            textFPS.text = "FPS: " + currentFPS.ToString(".0");
            timer += updateDelay;
        }
    }

    public void CalcFPS()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.01f;

        if (deltaTime > 0.0001f)
        {
            currentFPS = 1f / deltaTime;
        }
        else
        {
            currentFPS = 1f / 0.0001f;
        }
    }
}
