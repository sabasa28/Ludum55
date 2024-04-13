using UnityEngine;

using System.Collections.Generic;
using System;
using System.Linq;

public class HabilitySelectionHandler : MonoBehaviour
{
    [Header("General Data")]
    [SerializeField] private Transform holder = null;
    [SerializeField] private GameObject habilityPrefab = null;

    [Header("Configure Data")]
    [SerializeField] private List<HabilitySelectionConfigure> habilitySelectionConfigures = null;

    private List<HabilityButtonView> habilityButtons = null;
    private HabilityButtonView currentHability = null;

    private Action<Func<SelectionKeysConfigure>, Action, Action> onConfigureSelection = null;

    public void Initialize(Action<Func<SelectionKeysConfigure>, Action, Action> onConfigureSelection)
    {
        this.onConfigureSelection = onConfigureSelection;
        habilityButtons = new List<HabilityButtonView>();

        InstantiatePrefabs();
    }

    public void UpdateButtonsDetection()
    {
        for (int i = 0; i < habilityButtons.Count; i++)
        {
            if (Input.GetKeyDown(habilityButtons[i].ActivationKey))
            {
                currentHability.SelectView(false);

                habilityButtons[i].SelectView(true);
                currentHability = habilityButtons[i];

                onConfigureSelection.Invoke(GetCurrentSelectionKey, () => Debug.LogWarning("COMPLETE"), () => Debug.LogWarning("FAILURE"));
                return;
            }
        }
    }

    private SelectionKeysConfigure GetCurrentSelectionKey()
    {
        return currentHability.GetRandomSelectionKey();
    }

    private void InstantiatePrefabs()
    {
        for (int i = 0; i < habilitySelectionConfigures.Count; i++)
        {
            HabilityButtonView item = Instantiate(habilityPrefab, holder).GetComponent<HabilityButtonView>();

            item.Configure(habilitySelectionConfigures[i]);
            habilityButtons.Add(item);
        }

        currentHability = habilityButtons.First();
        currentHability.SelectView(true);
        onConfigureSelection.Invoke(GetCurrentSelectionKey, () => Debug.LogWarning("COMPLETE"), () => Debug.LogWarning("FAILURE"));
    }
}
