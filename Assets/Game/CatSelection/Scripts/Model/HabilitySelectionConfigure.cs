using UnityEngine;

using System.Collections.Generic;

[CreateAssetMenu(menuName = "HabilitySelection/HabilitySelectionConfigure")]
public class HabilitySelectionConfigure : ScriptableObject
{
    [SerializeField] private List<SelectionKeysConfigure> selectionKeysConfigure = null;
    [SerializeField] private Sprite habilityImage = null;
    [SerializeField] private KeyCode activationKey = KeyCode.None;
    [SerializeField] private GameObject catPrefab = null;

    public List<SelectionKeysConfigure> SelectionKeysConfigure { get => selectionKeysConfigure; }
    public Sprite HabilityImage { get => habilityImage; }
    public KeyCode ActivationKey { get => activationKey; }
    public GameObject CatPrefab { get => catPrefab; }
}
