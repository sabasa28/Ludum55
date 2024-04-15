using System.Collections;
using UnityEngine;

public class Rat : Entity
{
    [Header("Rat Variables")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private float TimeToSpawn = 0f;
    SpriteRenderer SpriteRend;
    public Entity EntityToChase;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip deathAudio = null;
    [SerializeField] private float pitchValue = 0.25f;


    private enum State
    { 
        Spawning,
        Chasing,
        Dying
    }
    private State CurrentState = State.Spawning;

    private const string deathTrigger = "death";
    private const float timeToDelete = 5f;

    private void Awake()
    {
        SpriteRend = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(SpawnCoroutine());
    }

    private void FixedUpdate()
    {
        switch (CurrentState)
        {
            case State.Spawning:
                //no deberia hacer nada aca, la corutina de spawn la dejo en el start porque asumo que siempre va a iniciar spawneando la rata
                break;
            case State.Chasing:
                MoveTowards(EntityToChase.transform.position);
                if (EntityToChase.transform.position.x < transform.position.x)
                {
                    if (!SpriteRend.flipX) SpriteRend.flipX = true;
                }
                else
                {
                    if (SpriteRend.flipX) SpriteRend.flipX = false;
                }
                break;
            case State.Dying:
                break;
            default:
                break;
        }
    }

    protected override void Die()
    {
        if (bIsAlive)
        {
            audioSource.clip = deathAudio;
            audioSource.pitch = pitchValue;
            audioSource.Play();

            animator.SetTrigger(deathTrigger);
            BoxCollider2D[] BoxCollider2Ds = GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D Coll in BoxCollider2Ds)
            { 
                Coll.enabled = false;
            }
            bIsAlive = false;
            CurrentState = State.Dying;
            GameController.Get().RatsKilled();

            Destroy(gameObject, timeToDelete);
        }
    }

    IEnumerator SpawnCoroutine()
    {
        float InitialTime = Time.time;
        float SpawnTime = Time.time + TimeToSpawn;
        while (Time.time < SpawnTime)
        {
            transform.localScale = Vector3.one * Utilities.InverseLerp(InitialTime, SpawnTime, Time.time);
            yield return null;
        }
        transform.localScale = Vector3.one;
        if (bIsAlive)
        { 
            CurrentState = State.Chasing;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //PLACEHOLDER para cuando choque contra un gato o algo
    {
        if (collision.CompareTag("CatAttack"))
        {
            TakeDamage(1);
        }
    }
}
