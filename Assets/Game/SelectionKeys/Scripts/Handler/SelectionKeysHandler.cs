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

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip wrongAudio = null;
    [SerializeField] private AudioClip correctAudio = null;

    private List<SelectionButtonView> selectionButtons = null;

    private int actualButton = 0;
    private int challengeAmount = 0;
    private bool loseSelection = false;

    private bool isActive = false;
    
    private char[] challengeKeys =
    {
        'W', 'A', 'S', 'D'
    };

    private Player playerRef = null;

    private Func<int> onGetCurrentSelectionKey = null;
    private Func<GameObject> onGetCurrentCatPrefab = null;
    private Action<GameObject> onSpawnCat = null;

    public bool IsActive { get => isActive; }

    public void Initialize(Player playerRef, Func<int> onGetCurrentSelectionKey, Func<GameObject> onGetCurrentCatPrefab, Action<GameObject> onSpawnCat)
    {
        this.playerRef = playerRef;
        this.onGetCurrentSelectionKey = onGetCurrentSelectionKey;
        this.onGetCurrentCatPrefab = onGetCurrentCatPrefab;
        this.onSpawnCat = onSpawnCat;

        selectionButtons = new List<SelectionButtonView>();
    }

    public void Configure()
    {
        challengeAmount = onGetCurrentSelectionKey.Invoke();

        InstatiateNewButtons();

        for (int i = 0; i < selectionButtons.Count; i++) 
        {
            if(i >= challengeAmount)
            {
                selectionButtons[i].gameObject.SetActive(false);
            }
            else
            {
                string key = challengeKeys[UnityEngine.Random.Range(0, challengeKeys.Length - 1)].ToString();

                selectionButtons[i].Configure(key);
                selectionButtons[i].gameObject.SetActive(true);
            }
        }
    }

    public void ToggleSelection()
    {
        if (isActive)
        {
            return;
        }
        else
        {
            playerRef.SetAnimationState(Player.TriggersAnimations.Casting);

            Configure();

            RestartButtons();
            ToggleHolder(true);
        }
    }

    public void UpdateSelection()
    {
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

            audioSource.clip = correctAudio;
            audioSource.Play();

            WinSelection();
        }
        else
        {
            selectionButtons[actualButton].UpdateState(SelectionButtonView.SelectionState.Wrong);
            loseSelection = true;
            actualButton = 0;
            
            audioSource.clip = wrongAudio;
            audioSource.Play();

            StartCoroutine(ClosePopUpTimer(
                () =>
                {

                    playerRef.SetStunState();
                    playerRef.SetAnimationState(Player.TriggersAnimations.WrongCat);
                }));
        }
    }

    public void ToggleHolder(bool state)
    {
        holder.gameObject.SetActive(state);
        isActive = state;
    }

    private bool GetValidKey()
    {
        for (int i = 0; i < challengeKeys.Length; i++)
        {
            if (Input.GetKeyDown(challengeKeys[i].ToString().ToLower()))
            {
                return true;
            }
        }

        return false;
    }

    private void WinSelection()
    {
        if (actualButton >= challengeAmount)
        {
            foreach (var item in selectionButtons)
            {
                if (item.gameObject.activeSelf)
                {
                    item.UpdateState(SelectionButtonView.SelectionState.Correct);
                }
            }

            StartCoroutine(ClosePopUpTimer(
                () =>
                {
                    playerRef.SetAnimationState(Player.TriggersAnimations.CorrectCat);
                    onSpawnCat.Invoke(onGetCurrentCatPrefab.Invoke());
                }));
        }
    }

    private void RestartButtons()
    {
        loseSelection = false;
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
