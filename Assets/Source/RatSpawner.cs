using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    [SerializeField]
    int RatsToSpawn;
    [SerializeField] //referido al tiempo que tarda la animacion de spawnear del spawner, no de las ratas que spawnea
    float TimeToSpawn;
    [SerializeField]
    float TimeBetweenRatSpawns;
    [SerializeField]
    Rat RatPrefabToInstantiate;
    //a quien seguirian las ratas spawneadas, es decir el player
    public Entity EntityToTarget;

    public void BeginSpawning()
    {
        StartCoroutine(SpawnRatsAndDisappearCoroutine());
    }

    IEnumerator SpawnRatsAndDisappearCoroutine()
    {
        float InitialTime = Time.time;
        float SpawnTime = Time.time + TimeToSpawn;
        //animacion de spawn
        while (Time.time < SpawnTime)
        {
            transform.localScale = Vector3.one * Utilities.InverseLerp(InitialTime, SpawnTime, Time.time);
            yield return null;
        }
        transform.localScale = Vector3.one;
        for (int i = 0; i < RatsToSpawn; i++)
        {
            Rat RatSpawned = Instantiate<Rat>(RatPrefabToInstantiate, transform.position, Quaternion.identity);
            RatSpawned.EntityToChase = EntityToTarget;
            yield return new WaitForSeconds(TimeBetweenRatSpawns);
        }
        InitialTime = Time.time;
        SpawnTime = Time.time + TimeToSpawn; //reuso el float pero es Despawn
        //animacion de despawn
        while (Time.time < SpawnTime)
        {
            transform.localScale = Vector3.one - Vector3.one * Utilities.InverseLerp(InitialTime, SpawnTime, Time.time);
            yield return null;
        }
        Destroy(gameObject);
    }
}
