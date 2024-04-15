using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Handlers")]
    [SerializeField] private SelectionKeysHandler selectionKeysHandler = null;
    [SerializeField] private HabilitySelectionHandler habilitySelectionHandler = null;
    [SerializeField] private CatSpawnerHandler catSpawnerHandler = null;
    [SerializeField] private RatSpawnerManager ratSpawnerManager = null;

    [Header("References")]
    [SerializeField] private Player player = null;
    [SerializeField] private UIGameplay UI = null;
    [SerializeField] private LifeHandler lifeHandler = null;

    [Header("UI Cursed")]
    [SerializeField] private TextMeshProUGUI ratsKilledText = null;

    private float TimeAtGameStart = 0.0f;
    private int ratsKilledAmount = 0;

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
        catSpawnerHandler.Initialize(player.transform, habilitySelectionHandler.ResetAllButtons);
        selectionKeysHandler.Initialize(player, habilitySelectionHandler.GetCurrentSelectionKey, habilitySelectionHandler.GetCurrentCatPrefab, catSpawnerHandler.GenerateCat);

        player.Configure(lifeHandler.UpdateLifes);
    }

    void Update()
    {
        habilitySelectionHandler.UpdateButtonsDetection(selectionKeysHandler.IsActive);
        selectionKeysHandler.UpdateSelection();
        catSpawnerHandler.UpdateInput();
    }

    public void EndGame()
    {
        ratSpawnerManager.StopSpawningRats();
        UI.DisplayEndgamePanel(ratsKilledAmount, Time.time - TimeAtGameStart);
    }

    public void RatsKilled()
    {
        ratsKilledAmount++;
        ratsKilledText.text = ratsKilledAmount.ToString();
    }
}
