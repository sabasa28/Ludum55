using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int InitialHealth;
    [SerializeField] protected float MovementSpeed;
    [SerializeField] protected int CurrentHealth;
    protected bool bIsAlive = true;
    protected virtual void Start()
    {
        CurrentHealth = InitialHealth;
    }

    protected virtual void TakeDamage(int DamageAmount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - DamageAmount, 0);
        if (CurrentHealth == 0)
        {
            Die();
        }
    }

    protected abstract void Die();

    protected virtual void SetMovementSpeed(float NewMovementSpeed)
    {
        MovementSpeed = Mathf.Max(NewMovementSpeed, 0.0f);
    }

    //devuelve true si alcanzo su objetivo
    protected virtual bool MoveTowards(Vector3 TargetPos)
    {
        if (Vector3.Distance(transform.position, TargetPos) < MovementSpeed * Time.fixedDeltaTime)
        {
            transform.position = TargetPos;
            return true;
        }

        Vector3 Direction = (TargetPos - transform.position).normalized;
        transform.position = transform.position + Direction * MovementSpeed * Time.fixedDeltaTime;
        return false;
    }
}
