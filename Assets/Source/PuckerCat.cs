using UnityEngine;

public class PuckerCat : Cat
{
    [SerializeField]
    PuckerCatProjectile ProjectilePrefab;
    protected override void FinishedSpawning()
    {
        audioSource.clip = abilityAudio;
        audioSource.Play();
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
