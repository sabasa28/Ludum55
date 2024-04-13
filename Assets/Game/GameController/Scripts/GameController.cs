using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SelectionKeysHandler selectionKeysHandler = null;
    [SerializeField] private HabilitySelectionHandler habilitySelectionHandler = null;

    void Start()
    {
        selectionKeysHandler.Initialize();
        
        habilitySelectionHandler.Initialize(selectionKeysHandler.Configure);
    }

    void Update()
    {
        habilitySelectionHandler.UpdateButtonsDetection();

        selectionKeysHandler.UpdateSelection();
    }
}
