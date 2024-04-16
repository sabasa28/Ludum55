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
    [SerializeField]
    Rat RedRatPrefabToInstantiate;
    [SerializeField]
    Rat ArmoredRatPrefabToInstantiate;
    [SerializeField]
    Rat RedArmoredRatPrefabToInstantiate;
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
            int Rand = Random.Range(0, 8);
            Rat Prefab;
            switch (Rand) //perdoname dios pues he pecado
            {
                case 0:
                case 1:
                case 2:
                    Prefab = RatPrefabToInstantiate;
                    break;
                case 3:
                case 4:
                case 5:
                    Prefab = RedRatPrefabToInstantiate;
                    break;
                case 6:
                    Prefab = ArmoredRatPrefabToInstantiate;
                    break;
                case 7:
                    Prefab = RedArmoredRatPrefabToInstantiate;
                    break;
                default:
                    Prefab = RatPrefabToInstantiate;
                    break;
            }
            Rat RatSpawned = Instantiate<Rat>(Prefab, transform.position, Quaternion.identity);
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
