using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected int InitialHealth;
    [SerializeField]
    protected float MovementSpeed;

    protected int CurrentHealth;

    protected virtual void TakeDamage(int DamageAmount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - DamageAmount, 0);
    }

    protected virtual void SetMovementSpeed(float NewMovementSpeed)
    {
        MovementSpeed = Mathf.Max(NewMovementSpeed, 0.0f);
    }
}
