using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cat : MonoBehaviour
{
    public Vector3 TargetPosition;
    SpriteRenderer SpriteRend;
    public enum State
    { 
        Spawning,
        Moving,
        Attacking,
        Dying
    }
    public State CurrentState = State.Spawning;

    protected virtual void Awake()
    {
        SpriteRend = GetComponentInChildren<SpriteRenderer>();
    }
    public virtual void Start()
    {
        if (TargetPosition.x < transform.position.x) SpriteRend.flipX = true;
    }

    protected abstract void FinishedSpawning();

}
