using TMPro;
using UnityEngine;

public class QAVersionTextUpdater : MonoBehaviour
{
    TextMeshProUGUI versionText;
    void Start()
    {
        versionText = GetComponent<TextMeshProUGUI>();
        versionText.text = Application.version;
    }
}
