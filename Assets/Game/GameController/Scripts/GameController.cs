using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SelectionKeysHandler selectionKeysHandler = null;
    [Space]
    [Header("TEST")]
    [SerializeField] private SelectionKeysConfigure configureTest = null;

    void Start()
    {
        selectionKeysHandler.Initialize();
        selectionKeysHandler.Configure(configureTest, () => Debug.LogWarning("COMPLETE"), () => Debug.LogWarning("FAILURE"));
    }

    void Update()
    {
        selectionKeysHandler.UpdateSelection();
    }
}
