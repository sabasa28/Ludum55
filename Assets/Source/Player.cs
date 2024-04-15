using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("Player Variables")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private SpriteRenderer characterSprite = null;
    [Space]
    [SerializeField] Color invulnerabilityColor = Color.red;
    [SerializeField] float invulnerabilityFramesTime = 0.5f;
    [SerializeField] float stunFramesTime = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip punchAudio = null;

    private Vector3 desiredPosition;
    private bool bIsMoving;
    private bool bIsVulnerable = true;
    private bool isStunned = false;

    private Camera CameraToUse;

    private Action<int> onUpdateUI = null;

    private void Awake()
    {
        CameraToUse = Camera.main;
    }

    private const string isWalking = "isWalking";
    private const string stopStun = "stopStun";
    private const string casting = "casting";
    private const string correctCat = "correctCat";
    private const string wrongCat = "wrongCat";

    public enum TriggersAnimations
    {
        StopStun,
        Casting,
        CorrectCat,
        WrongCat
    }

    public void Configure(Action<int> onUpdateUI)
    {
        this.onUpdateUI = onUpdateUI;
    }

    public void SetStunState()
    {
        IEnumerator Timer()
        {
            yield return new WaitForSeconds(stunFramesTime);

            animator.SetTrigger(stopStun);
            isStunned = false;
        }

        desiredPosition = Vector3.zero;
        bIsMoving = false;
        isStunned = true;

        StartCoroutine(Timer());
    }

    public void ResetAnimationState()
    {
        animator.Play("Idle");
    }

    public void SetAnimationState(TriggersAnimations state)
    {
        switch (state)
        {
            case TriggersAnimations.StopStun:
                animator.SetTrigger(stopStun);
                break;
            case TriggersAnimations.Casting:
                animator.SetTrigger(casting);
                break;
            case TriggersAnimations.CorrectCat:
                animator.SetTrigger(correctCat);
                break;
            case TriggersAnimations.WrongCat:
                animator.SetTrigger(wrongCat);
                break;
        }
    }

    protected override void TakeDamage(int DamageAmount)
    {
        onUpdateUI.Invoke(DamageAmount);

        base.TakeDamage(DamageAmount);
    }

    protected override void Die()
    {
        if (bIsAlive)
        {
            bIsAlive = false;
            GameController.Get().EndGame();
        }
    }

    private void Update()
    {
        CatchInput();
    }

    private void FixedUpdate()
    {
        if (!isStunned && bIsMoving && bIsAlive)
        {
            bIsMoving = !MoveTowards(desiredPosition);

            if(!bIsMoving)
            {
                animator.SetBool(isWalking, bIsMoving);
            }
        }
    }

    private void CatchInput()
    {
        if (!isStunned && bIsAlive && Input.GetButtonDown("Move"))
        {
            desiredPosition = Utilities.GetMousePositionInWorld(CameraToUse);
            bIsMoving = true;

            characterSprite.flipX = transform.position.x > desiredPosition.x;

            animator.SetBool(isWalking, bIsMoving);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (bIsVulnerable)
            {
                audioSource.clip = punchAudio;
                audioSource.Play();

                TakeDamage(1); //tenemos un solo enemigo y siempre mete 1 de da�o, me tomo la libertad de magic numberearlo
                StartCoroutine(InvulnerabilityFrames());
            }
        }
    }

    private IEnumerator InvulnerabilityFrames()
    {
        bIsVulnerable = false;

        Color startColor = characterSprite.color;
        float elapsedFlashTime = 0;
        float elapsedFlashPersentage = 0;

        while (elapsedFlashTime < invulnerabilityFramesTime)
        {
            elapsedFlashTime += Time.deltaTime;
            elapsedFlashPersentage = elapsedFlashTime / invulnerabilityFramesTime;

            if(elapsedFlashPersentage > 1)
            {
                elapsedFlashPersentage = 1;
            }

            float pingpongPercentage = Mathf.PingPong(elapsedFlashPersentage * 2 * 5, 1);
            characterSprite.color = Color.Lerp(startColor, invulnerabilityColor, pingpongPercentage);

            yield return null;
        }
        
        bIsVulnerable = true;
    }
}
