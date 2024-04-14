using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Entity
{
    [SerializeField]
    float TimeToSpawn;

    public Entity EntityToChase;

    enum State
    { 
        Spawning,
        Chasing,
        Dying
    }
    State CurrentState = State.Spawning;

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
        CurrentState = State.Dying;
        Destroy(gameObject);
        //REMOVER CUANDO AGREGUEMOS LA ANIMACION
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
        //if (collision.CompareTag("AAAAAAAAAAAAAAAAA"))
        //{
        //    TakeDamage(1);
        //}
    }
}
