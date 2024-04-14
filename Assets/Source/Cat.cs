using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cat : MonoBehaviour
{
    public Vector3 TargetPosition;

    public enum State
    { 
        Spawning,
        Moving,
        Attacking,
        Dying
    }
    public State CurrentState = State.Spawning;

    protected abstract void FinishedSpawning();

}
