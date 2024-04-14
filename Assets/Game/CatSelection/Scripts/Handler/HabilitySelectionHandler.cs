using UnityEngine;

using System.Collections.Generic;
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

    public void Initialize()
    {
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
                return;
            }
        }
    }

    public SelectionKeysConfigure GetCurrentSelectionKey()
    {
        return currentHability.GetRandomSelectionKey();
    }

    public GameObject GetCurrentCatPrefab()
    {
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
        currentHability.SelectView(true);
    }
}
