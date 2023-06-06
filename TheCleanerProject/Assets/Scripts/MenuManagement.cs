using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagement : MonoBehaviour
{

    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject menuPanel;
    public GameObject confirmationPanel;
    bool optionsOpen;
    bool creditsOpen;
    bool confirmationOpen;

    private void Awake()
    {
        Close();
    }

    private void Update()
    {
        if ((optionsOpen || creditsOpen) && Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
            Debug.Log("Returning Menu");
        }
    }

    public void Options()
    {
        if (optionsPanel != null && menuPanel != null && !optionsOpen)
        {
            menuPanel.SetActive(false);
            optionsPanel.SetActive(true);

            optionsOpen = true;
            Debug.Log("Open Options");
        }
    }

    public void Credits()
    {
        if (creditsPanel != null && menuPanel != null && !creditsOpen)
        {
            menuPanel.SetActive(false);
            creditsPanel.SetActive(true);

            creditsOpen = true;
            Debug.Log("Open Credits");
        }
    }

    public void Close()
    {
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        confirmationPanel.SetActive(false);
        menuPanel.SetActive(true);

        optionsOpen = false;
        creditsOpen = false;
        confirmationOpen = false;
    }

    public void Confirmation()
    {
        if (confirmationPanel != null && menuPanel != null && !confirmationOpen)
        {
            menuPanel.SetActive(false);
            confirmationPanel.SetActive(true);

            confirmationOpen = true;
            Debug.Log("Open Confirmation");
        }
    }
}

    
