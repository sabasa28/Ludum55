using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SelectionKeysHandler : MonoBehaviour
{
    [Header("General Data")]
    [SerializeField] private Transform holder = null;
    [SerializeField] private Transform holderPrefabs = null;

    [Header("Buttons Data")]
    [SerializeField] private GameObject buttonPrefab = null;
    
    [Header("Popup Data")]
    [SerializeField] private float closePopupTimer = 0.15f;
    
    private List<SelectionButtonView> selectionButtons = null;

    private int actualButton = 0;
    private int challengeAmount = 0;
    private bool loseSelection = false;

    private bool isActive = false;

    private char[] keys =
    {
        'Q','W','E','R','T','Y','U','I','O','P',
        'A','S','D','F','G','H','J','K','L',
        'Z','X','C','V','B','N','M'
    };

    private Func<SelectionKeysConfigure> onGetCurrentSelectionKey = null;
    private Func<GameObject> onGetCurrentCatPrefab = null;
    private Action<GameObject> onSpawnCat = null;

    public void Initialize(Func<SelectionKeysConfigure> onGetCurrentSelectionKey, Func<GameObject> onGetCurrentCatPrefab, Action<GameObject> onSpawnCat)
    {
        this.onGetCurrentSelectionKey = onGetCurrentSelectionKey;
        this.onGetCurrentCatPrefab = onGetCurrentCatPrefab;
        this.onSpawnCat = onSpawnCat;

        selectionButtons = new List<SelectionButtonView>();
    }

    public void Configure()
    {
        SelectionKeysConfigure callengeConfigure = onGetCurrentSelectionKey.Invoke();
        challengeAmount = callengeConfigure.SequenseOfKeys.Count;

        InstatiateNewButtons();

        for (int i = 0; i < selectionButtons.Count; i++) 
        {
            if(i >= challengeAmount)
            {
                selectionButtons[i].gameObject.SetActive(false);
            }
            else
            {
                selectionButtons[i].Configure(callengeConfigure.SequenseOfKeys[i]);
                selectionButtons[i].gameObject.SetActive(true);
            }
        }
    }

    public void UpdateSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isActive)
            {
                ToggleHolder(false);
            }
            else
            {
                Configure();

                RestartButtons();
                ToggleHolder(true);
            }
            return;
        }

        if (loseSelection || !isActive) 
        {
            return;
        }

        if(!Input.anyKeyDown || !GetValidKey())
        {
            return;
        }        

        if (Input.GetKeyDown(selectionButtons[actualButton].PersonalKeyCode))
        {
            selectionButtons[actualButton].UpdateState(SelectionButtonView.SelectionState.Correct);
            actualButton++;

            WinSelection();
        }
        else
        {
            selectionButtons[actualButton].UpdateState(SelectionButtonView.SelectionState.Wrong);
            loseSelection = true;
            actualButton = 0;

            StartCoroutine(ClosePopUpTimer(null));
        }
    }

    public void ToggleHolder(bool state)
    {
        holder.gameObject.SetActive(state);
        isActive = state;
    }

    private bool GetValidKey()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i].ToString().ToLower()))
            {
                return true;
            }
        }

        return false;
    }

    private void WinSelection()
    {
        if(actualButton >= challengeAmount)
        {
            foreach (var item in selectionButtons)
            {
                if (item.gameObject.activeSelf)
                {
                    item.UpdateState(SelectionButtonView.SelectionState.Correct);
                }
            }

            StartCoroutine(ClosePopUpTimer(() => onSpawnCat.Invoke(onGetCurrentCatPrefab.Invoke())));
        }
    }

    private void RestartButtons()
    {
        actualButton = 0;

        foreach (var item in selectionButtons)
        {
            item.UpdateState(SelectionButtonView.SelectionState.Idle);
        }
    }

    private void InstatiateNewButtons()
    {
        if (challengeAmount > selectionButtons.Count)
        {
            int newAmount = challengeAmount - selectionButtons.Count;

            for (int i = 0; i < newAmount; i++)
            {
                selectionButtons.Add(Instantiate(buttonPrefab, holderPrefabs.transform).GetComponent<SelectionButtonView>());
            }
        }
    }

    private IEnumerator ClosePopUpTimer(Action onComplete)
    {
        yield return new WaitForSeconds(closePopupTimer);

        ToggleHolder(false);

        onComplete?.Invoke();

        yield return null;
    }
}
