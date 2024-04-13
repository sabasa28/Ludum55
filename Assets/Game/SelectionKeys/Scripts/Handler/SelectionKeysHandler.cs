using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SelectionKeysHandler : MonoBehaviour
{
    [Header("General Data")]
    [SerializeField] private Transform holder = null;

    [Header("Buttons Data")]
    [SerializeField] private GameObject buttonPrefab = null;
    
    [Header("Popup Data")]
    [SerializeField] private float closePopupTimer = 0.15f;
    
    private List<SelectionButtonView> selectionButtons = null;

    private int actualButton = 0;
    private int challengeAmount = 0;
    private bool loseSelection = false;

    private bool isActive = false;

    private Action onComplete = null;
    private Action onFailure = null;

    public void Initialize()
    {
        selectionButtons = new List<SelectionButtonView>();
    }

    public void Configure(SelectionKeysConfigure selectionKeysConfigure, Action onComplete = null, Action onFailure = null)
    {
        this.onComplete = onComplete;
        this.onFailure = onFailure;

        challengeAmount = selectionKeysConfigure.SequenseOfKeys.Count;

        InstatiateNewButtons();

        for (int i = 0; i < selectionButtons.Count; i++) 
        {
            if(i >= challengeAmount)
            {
                selectionButtons[i].gameObject.SetActive(false);
            }
            else
            {
                selectionButtons[i].Configure(selectionKeysConfigure.SequenseOfKeys[i]);
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
                RestartButtons();
                ToggleHolder(true);
            }
            return;
        }

        if (loseSelection || !isActive) 
        {
            return;
        }

        if(!Input.anyKeyDown || Input.GetKeyDown(KeyCode.Escape))
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

            StartCoroutine(ClosePopUpTimer(onFailure));
        }
    }

    public void ToggleHolder(bool state)
    {
        holder.gameObject.SetActive(state);
        isActive = state;
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

            StartCoroutine(ClosePopUpTimer(onComplete));
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
                selectionButtons.Add(Instantiate(buttonPrefab, holder.transform).GetComponent<SelectionButtonView>());
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
