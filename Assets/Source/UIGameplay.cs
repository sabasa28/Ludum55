using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class UIGameplay : MonoBehaviour
{
    [SerializeField] private GameObject EndgamePanel = null;
    [SerializeField] private TextMeshProUGUI RatsKilledText = null;
    [SerializeField] private TextMeshProUGUI TimeAliveText = null;
    [Space]
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip loseAudio = null;

    public void DisplayEndgamePanel(int RatsKilled, float SecondsAlive)
    {
        RatsKilledText.text = "Rats killed = " + RatsKilled;
        TimeAliveText.text = "Seconds alive = " + (int)SecondsAlive;
        EndgamePanel.SetActive(true);

        audioSource.clip = loseAudio;
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
