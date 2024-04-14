using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIGameplay : MonoBehaviour
{
    [SerializeField]
    GameObject EndgamePanel;
    [SerializeField]
    TextMeshProUGUI RatsKilledText;
    [SerializeField]
    TextMeshProUGUI TimeAliveText;

    public void DisplayEndgamePanel(int RatsKilled, float SecondsAlive)
    {
        RatsKilledText.text = "Rats killed = " + RatsKilled;
        TimeAliveText.text = "Seconds alive = " + (int)SecondsAlive;
        EndgamePanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
