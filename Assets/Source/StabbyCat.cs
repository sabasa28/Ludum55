using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabbyCat : Cat
{
    [SerializeField] float MovementSpeed;
    [SerializeField] float MaxWalkTime;
    [SerializeField] ChunkyCatAOE TriggerForDamage;
    Animator AnimController;
    bool bReachedEnemy = false;
    protected override void Awake()
    {
        base.Awake();
        AnimController = GetComponent<Animator>();
    }

    public override void Start()
    {
        if (TargetPosition.x < transform.position.x) transform.rotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);
    }

    protected override void FinishedSpawning()
    {
        ChangeCurrentState(State.Moving);
    }

    public void FinishedChargingAttack()
    {
        TriggerForDamage.ActivateDamage();
    }

    public void FinishedAttacking()
    {
        TriggerForDamage.DeactivateDamage();
    }

    public void IsReadyToDespawn()
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
                StartCoroutine(WalkUntilCollisionOrTime());
                break;
            case State.Attacking:
                AnimController.SetTrigger("Attack");
                break;
            case State.Dying:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    IEnumerator WalkUntilCollisionOrTime()
    {
        float Timer = 0.0f;
        while (Timer < MaxWalkTime)
        {
            Timer += Time.deltaTime;
            if (bReachedEnemy)
            {
                break;
            }
            MoveTowards(TargetPosition);

            yield return null;
        }
        ChangeCurrentState(State.Attacking);
    }

    protected virtual bool MoveTowards(Vector3 TargetPos)
    {
        if (Vector3.Distance(transform.position, TargetPos) < MovementSpeed * Time.deltaTime)
        {
            transform.position = TargetPos;
            return true;
        }

        Vector3 Direction = (TargetPos - transform.position).normalized;
        transform.position = transform.position + Direction * MovementSpeed * Time.deltaTime;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            bReachedEnemy = true;
        }
    }

}
