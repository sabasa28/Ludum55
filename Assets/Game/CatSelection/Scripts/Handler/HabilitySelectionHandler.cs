using UnityEngine;

using System.Collections.Generic;
using System.Linq;
using System;

public class HabilitySelectionHandler : MonoBehaviour
{
    [Header("General Data")]
    [SerializeField] private Transform holder = null;
    [SerializeField] private GameObject habilityPrefab = null;

    [Header("Configure Data")]
    [SerializeField] private List<HabilitySelectionConfigure> habilitySelectionConfigures = null;

    private List<HabilityButtonView> habilityButtons = null;
    private HabilityButtonView currentHability = null;
    private HabilityButtonView oldCurrentHability = null;

    private Action onToggleSelection = null;

    public void Initialize(Action onToggleSelection)
    {
        this.onToggleSelection = onToggleSelection;

        habilityButtons = new List<HabilityButtonView>();

        InstantiatePrefabs();
    }

    public void UpdateButtonsDetection(bool isChallengeActive)
    {
        if(isChallengeActive)
        {
            return;
        }

        for (int i = 0; i < habilityButtons.Count; i++)
        {
            if (Input.GetKeyDown(habilityButtons[i].ActivationKey))
            {
                oldCurrentHability = currentHability;

                habilityButtons[i].SelectView();
                currentHability = habilityButtons[i];

                onToggleSelection.Invoke();
                return;
            }
        }
    }

    public int GetCurrentSelectionKey()
    {
        return currentHability.GetAmountSelectionKey();
    }

    public GameObject GetCurrentCatPrefab()
    {
        oldCurrentHability?.ToggleBackground(false);
        currentHability.ToggleBackground(true);

        return currentHability.CatPrefab;
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
        oldCurrentHability = currentHability;
    }
}
