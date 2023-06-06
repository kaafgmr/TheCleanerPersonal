using TMPro;
using UnityEngine;

public class WinLoseDetector : MonoBehaviour
{
    public TextMeshProUGUI winLoseText;
    public GameObject creditsButton;

    void Start()
    {
        if (PlayerPrefs.GetInt("Win") == 1)
        {
            winLoseText.text = "You Win";
            creditsButton.SetActive(true);
        }
        else
        {
            winLoseText.text = "Game Over";
            creditsButton.SetActive(false);
        }
    }
}
