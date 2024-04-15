using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject CreditsPanel = null;
    [SerializeField] private GameObject ControlsPanel = null;
    [SerializeField] GameObject LorePanel;
    [SerializeField] float TimeDisplayingLore;
    
    public void GoToGameplayScene()
    {
        StartCoroutine(DisplayLoreScreen());
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

    IEnumerator DisplayLoreScreen()
    {
        LorePanel.SetActive(LorePanel);
        yield return new WaitForSeconds(TimeDisplayingLore);
        SceneManager.LoadScene("Gameplay");
    }
}
