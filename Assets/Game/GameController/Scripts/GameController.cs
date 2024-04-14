using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] private SelectionKeysHandler selectionKeysHandler = null;
    [SerializeField] private HabilitySelectionHandler habilitySelectionHandler = null;
    [SerializeField] private CatSpawnerHandler catSpawnerHandler = null;

    [Header("References")]
    [SerializeField] private Player player = null;
    [SerializeField] private UIGameplay UI = null;

    float TimeAtGameStart = 0.0f;
    public int RatsKilled = 0;

    private static GameController Instance;
    public static GameController Get()
    {
        return Instance;
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TimeAtGameStart = Time.time;
        habilitySelectionHandler.Initialize(selectionKeysHandler.ToggleSelection);
        catSpawnerHandler.Initialize(player.transform);
        selectionKeysHandler.Initialize(player, habilitySelectionHandler.GetCurrentSelectionKey, habilitySelectionHandler.GetCurrentCatPrefab, catSpawnerHandler.GenerateCat);
    }

    void Update()
    {
        habilitySelectionHandler.UpdateButtonsDetection(selectionKeysHandler.IsActive);
        selectionKeysHandler.UpdateSelection();
        catSpawnerHandler.UpdateInput();
    }

    public void EndGame()
    {
        UI.DisplayEndgamePanel(RatsKilled, Time.time - TimeAtGameStart);
    }
}
