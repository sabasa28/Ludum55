using UnityEngine;

using System.Collections.Generic;

[CreateAssetMenu(menuName = "Selectionkeys/SelectionKeysConfigure")]
public class SelectionKeysConfigure : ScriptableObject
{
    [SerializeField] private List<KeyCode> sequenseOfKeys = null;

    public List<KeyCode> SequenseOfKeys { get => sequenseOfKeys; }
}
