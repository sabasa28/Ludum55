using System.Collections;
using UnityEngine;

public class Rat : Entity
{
    [Header("Rat Variables")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private float TimeToSpawn = 0f;

    public Entity EntityToChase;

    private enum State
    { 
        Spawning,
        Chasing,
        Dying
    }
    private State CurrentState = State.Spawning;

    private const string deathTrigger = "death";
    private const float timeToDelete = 5f;

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
                break;
            case State.Dying:
                //desactivar collision para que no inflija daño y hacer animacion de muerte
                break;
            default:
                break;
        }
    }

    protected override void Die()
    {
        if (bIsAlive)
        {
            animator.SetTrigger(deathTrigger);

            bIsAlive = false;
            CurrentState = State.Dying;
            GameController.Get().RatsKilled();

            Destroy(gameObject, timeToDelete); //REMOVER CUANDO AGREGUEMOS LA ANIMACION
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
        CurrentState = State.Chasing;
    }

    private void OnTriggerEnter2D(Collider2D collision) //PLACEHOLDER para cuando choque contra un gato o algo
    {
        if (collision.CompareTag("CatAttack"))
        {
            TakeDamage(1);
        }
    }
}
