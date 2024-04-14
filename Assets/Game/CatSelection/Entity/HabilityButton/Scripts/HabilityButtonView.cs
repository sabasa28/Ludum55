using UnityEngine;
using UnityEngine.UI;

public class HabilityButtonView : MonoBehaviour
{
    [SerializeField] private GameObject background = null;
    [SerializeField] private Image image = null;

    private int amountSelectionKey = 0;
    private KeyCode activationKey = KeyCode.None;
    private GameObject catPrefab = null;

    public KeyCode ActivationKey { get => activationKey; }
    public GameObject CatPrefab { get => catPrefab; }

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

    public void SelectView(bool state)
    {
        background.SetActive(state);
    }
}
