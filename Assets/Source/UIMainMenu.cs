using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject CreditsPanel = null;
    [SerializeField] private GameObject ControlsPanel = null;

    public void GoToGameplayScene()
    {
        SceneManager.LoadScene("Gameplay");
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
