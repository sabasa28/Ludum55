using UnityEngine;

using System.Collections.Generic;

[CreateAssetMenu(menuName = "HabilitySelection/HabilitySelectionConfigure")]
public class HabilitySelectionConfigure : ScriptableObject
{
    [SerializeField] private int amountOfKeys = 0;
    [SerializeField] private Sprite habilityImage = null;
    [SerializeField] private KeyCode activationKey = KeyCode.None;
    [SerializeField] private GameObject catPrefab = null;

    public int AmountOfKeys { get => amountOfKeys; }
    public Sprite HabilityImage { get => habilityImage; }
    public KeyCode ActivationKey { get => activationKey; }
    public GameObject CatPrefab { get => catPrefab; }
}
