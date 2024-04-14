using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HabilityButtonView : MonoBehaviour
{
    [SerializeField] private GameObject background = null;
    [SerializeField] private Image image = null;

    private List<SelectionKeysConfigure> selectionKeysConfigure = null;
    private KeyCode activationKey = KeyCode.None;
    private GameObject catPrefab = null;

    public KeyCode ActivationKey { get => activationKey; }
    public GameObject CatPrefab { get => catPrefab; }

    public void Configure(HabilitySelectionConfigure habilitySelectionConfigure)
    {
        image.sprite = habilitySelectionConfigure.HabilityImage;
        selectionKeysConfigure = habilitySelectionConfigure.SelectionKeysConfigure;
        activationKey = habilitySelectionConfigure.ActivationKey;
        catPrefab = habilitySelectionConfigure.CatPrefab;
    }

    public SelectionKeysConfigure GetRandomSelectionKey()
    {
        return selectionKeysConfigure[Random.Range(0, selectionKeysConfigure.Count - 1)];
    }

    public void SelectView(bool state)
    {
        background.SetActive(state);
    }
}
