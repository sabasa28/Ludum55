using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject CreditsPanel;
    [SerializeField]
    GameObject ControlsPanel;

    public void GoToGameplayScene()
    {
        SceneManager.LoadScene("Iñaki Test");
    }
    public void SetCreditsPanelVisibility(bool IsVisible)
    {
        CreditsPanel.SetActive(IsVisible);
    }
    public void SetControlsPanelVisibility(bool IsVisible)
    {
        ControlsPanel.SetActive(IsVisible);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
