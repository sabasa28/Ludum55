using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkyCat : Cat
{
    [SerializeField] ChunkyCatAOE TriggerForDamage;
    protected override void FinishedSpawning()
    {
        ChangeCurrentState(State.Moving);
    }

    protected void FinishedMoving()
    {
        ChangeCurrentState(State.Attacking);
    }
    protected void FinishedAttacking()
    {
        ChangeCurrentState(State.Dying);
    }
    void ChangeCurrentState(State NewState)
    {
        CurrentState = NewState;
        switch (CurrentState)
        {
            case State.Spawning:
                break;
            case State.Moving:
                StartCoroutine(MoveToTargetCoroutine());
                break;
            case State.Attacking:
                TriggerForDamage.ActivateDamage();
                break;
            case State.Dying:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    IEnumerator MoveToTargetCoroutine()
    {
        Vector3 InitialPosition = transform.position;
        float Timer = 0.0f;
        while (Timer < 1)
        {
            Timer += Time.deltaTime;
            transform.position = Vector3.Lerp(InitialPosition, TargetPosition, Timer); //esto funciona porque el tiempo al que hay que llegar es 1, si lo cambio hay que hacerlo bien
            yield return null;
        }
        ChangeCurrentState(State.Attacking);
    }
}
