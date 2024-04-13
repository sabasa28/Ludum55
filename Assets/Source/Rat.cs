using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Entity
{
    [SerializeField]
    float TimeToSpawn;

    enum State
    { 
        Spawning,
        Chasing,
        Dying
    }
    State CurrentState = State.Spawning;

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    void Update()
    {
        
    }

    float InverseLerp(float a, float b, float v)
    {
        return (v - a) / (b - a);
    }

    IEnumerator SpawnCoroutine()
    {
        float InitialTime = Time.time;
        float SpawnTime = Time.time + TimeToSpawn;
        while (Time.time < SpawnTime)
        {
            transform.localScale *= InverseLerp(InitialTime, SpawnTime, Time.time);
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
}
