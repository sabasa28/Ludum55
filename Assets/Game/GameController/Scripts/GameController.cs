using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] private SelectionKeysHandler selectionKeysHandler = null;
    [SerializeField] private HabilitySelectionHandler habilitySelectionHandler = null;
    [SerializeField] private CatSpawnerHandler catSpawnerHandler = null;

    [Header("Handlers")]
    [SerializeField] private Player player = null;

    void Start()
    {
        habilitySelectionHandler.Initialize();
        catSpawnerHandler.Initialize(player.transform);
        selectionKeysHandler.Initialize(habilitySelectionHandler.GetCurrentSelectionKey, habilitySelectionHandler.GetCurrentCatPrefab, catSpawnerHandler.GenerateCat);
    }

    void Update()
    {
        habilitySelectionHandler.UpdateButtonsDetection();
        selectionKeysHandler.UpdateSelection();
        catSpawnerHandler.UpdateInput();
    }
}
