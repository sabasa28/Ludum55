using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckerCat : Cat
{
    [SerializeField]
    PuckerCatProjectile ProjectilePrefab;
    protected override void FinishedSpawning()
    {
        //este no se mueve asi que no hace nada despues de spawnear
    }
    public void FinishedChargingAttack()
    {
        Vector3 MoveDirection = (TargetPosition - transform.position).normalized;
        PuckerCatProjectile SpawnedProjectile = Instantiate<PuckerCatProjectile>(ProjectilePrefab, transform.position, Quaternion.identity);
        SpawnedProjectile.TargetDirection = MoveDirection;
    }

    protected void FinishedAttacking()
    {
        Destroy(gameObject);
    }
}
