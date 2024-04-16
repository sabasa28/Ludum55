using UnityEngine;

public abstract class Cat : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] protected AudioSource audioSource = null;
    [SerializeField] protected AudioClip abilityAudio = null;

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
