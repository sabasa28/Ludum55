using UnityEngine;
using UnityEngine.UI;

public class HabilityButtonView : MonoBehaviour
{
    [SerializeField] private Image image = null;
    [SerializeField] private Image background = null;
    [SerializeField] private Animator animator = null;

    private int amountSelectionKey = 0;
    private KeyCode activationKey = KeyCode.None;
    private GameObject catPrefab = null;

    public KeyCode ActivationKey { get => activationKey; }
    public GameObject CatPrefab { get => catPrefab; }

    private const string TouchAnimation = "TouchAnimation";

    public void Configure(HabilitySelectionConfigure habilitySelectionConfigure)
    {
        image.sprite = habilitySelectionConfigure.HabilityImage;
        amountSelectionKey = habilitySelectionConfigure.AmountOfKeys;
        activationKey = habilitySelectionConfigure.ActivationKey;
        catPrefab = habilitySelectionConfigure.CatPrefab;
    }

    public int GetAmountSelectionKey()
    {
        return amountSelectionKey;
    }

    public void ToggleBackground(bool state)
    {
        background.enabled = state;
    }

    public void SelectView()
    {
        animator.Play(TouchAnimation);
    }
}
