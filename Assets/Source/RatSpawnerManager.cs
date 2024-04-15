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

    [SerializeField]
    Transform LeftWall;
    [SerializeField]
    Transform RightWall;
    [SerializeField]
    Transform BottomWall;
    [SerializeField]
    Transform TopWall;

    [SerializeField]
    float WallCheckOffset;


    private void Start()
    {
        StartCoroutine(SpawnPortals());
    }

    public void StopSpawningRats()
    {
        StopAllCoroutines();
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

    Vector3 GetPortalRandomSpawnPosition() 
    {
        Vector3 DirectionToSpawn = new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized;
        Vector3 PositionToSpawn = PlayerReference.transform.position + DirectionToSpawn * Random.Range(MinSpawnDistanceFromPlayer, MaxSpawnDistanceFromPlayer);

        if (PositionToSpawn.x < LeftWall.position.x + WallCheckOffset)
        { 
            PositionToSpawn.x = LeftWall.position.x + WallCheckOffset;
        }
        else if (PositionToSpawn.x > RightWall.position.x - WallCheckOffset)
        {
            PositionToSpawn.x = RightWall.position.x - WallCheckOffset;
        }

        if (PositionToSpawn.y < BottomWall.position.y + WallCheckOffset)
        { 
            PositionToSpawn.y = BottomWall.position.y + WallCheckOffset;
        }
        else if (PositionToSpawn.y > TopWall.position.y - WallCheckOffset)
        {
            PositionToSpawn.y = TopWall.position.y - WallCheckOffset;
        }

        return PositionToSpawn; 
    }
}
