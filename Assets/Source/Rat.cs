using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [SerializeField]

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

    IEnumerator SpawnCoroutine()
    {

        return null;
    }
}
