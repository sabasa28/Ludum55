using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawnerManager : MonoBehaviour
{
    [SerializeField]
    float TimeBetweenPortals;
    [SerializeField]
    float MultiplierForUpdatingTBP;
    [SerializeField]
    float MinTBP;
    [SerializeField]
    RatSpawner PortalPrefab;
    [SerializeField]
    Player PlayerReference;
    [SerializeField]
    float MinSpawnDistanceFromPlayer;
    [SerializeField]
    float MaxSpawnDistanceFromPlayer;


    private void Start()
    {
        StartCoroutine(SpawnPortals());
    }

    IEnumerator SpawnPortals()
    {
        while (true) // >:)
        {
            if (TimeBetweenPortals > MinTBP)
            { 
                TimeBetweenPortals = Mathf.Max (TimeBetweenPortals * MultiplierForUpdatingTBP, MinTBP);
            }
            RatSpawner SpawnedPortal = Instantiate<RatSpawner>(PortalPrefab, GetPortalRandomSpawnPosition(), Quaternion.identity);
            SpawnedPortal.EntityToTarget = PlayerReference;
            SpawnedPortal.BeginSpawning();
            yield return new WaitForSeconds(TimeBetweenPortals);
        }
    }

    Vector3 GetPortalRandomSpawnPosition() //despues, si agregamos paredes vamos a querer checkear que esta posicion no este afuera de los limites del mapa
    {
        Vector3 DirectionToSpawn = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized;
        return PlayerReference.transform.position + DirectionToSpawn * Random.Range(MinSpawnDistanceFromPlayer, MaxSpawnDistanceFromPlayer); 
    }
}
