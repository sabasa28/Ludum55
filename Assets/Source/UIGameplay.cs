using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIGameplay : MonoBehaviour
{
    [SerializeField] private GameObject EndgamePanel = null;
    [SerializeField] private TextMeshProUGUI RatsKilledText = null;
    [SerializeField] private TextMeshProUGUI TimeAliveText = null;

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
