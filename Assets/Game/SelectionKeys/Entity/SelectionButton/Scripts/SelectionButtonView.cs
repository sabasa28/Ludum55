using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class SelectionButtonView : MonoBehaviour
{
    [SerializeField] private Image image = null;
    [SerializeField] private TextMeshProUGUI txtKey = null;
    [SerializeField] private Animator animator = null;
    [Space]
    [SerializeField] private Color idleColor = Color.white;
    [SerializeField] private Color correctColor = Color.white;
    [SerializeField] private Color wrongColor = Color.white;

    private string personalKeyCode = null;

    public string PersonalKeyCode { get => personalKeyCode; }

    public enum SelectionState
    {
        Idle,
        Correct,
        Wrong
    }

    private const string correctTriggerAnim = "selectionButton_correct";
    private const string wrongTriggerAnim = "selectionButton_wrong";
    private const string startAnim = "start";

    public void Configure(string personalKey)
    {
        txtKey.text = personalKey;
        personalKeyCode = personalKey.ToLower();
    }

    public void UpdateState(SelectionState state)
    {
        switch (state) 
        {
            case SelectionState.Idle:
                image.color = idleColor;
                break;
            case SelectionState.Correct:
                image.color = correctColor;
                animator.Play(correctTriggerAnim);
                break;
            case SelectionState.Wrong:
                image.color = wrongColor;
                animator.Play(wrongTriggerAnim);
                break;
        }
    }

    #region ANIMATIONS_METHODS
    private void ResetAnimator()
    {
        animator.Play(startAnim);
    }
    #endregion
}
