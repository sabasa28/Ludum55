using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    Vector3 DesiredPosition;
    bool bIsMoving;
    bool bIsVulnerable = true;

    [SerializeField]
    float InvulnerabilityFramesTime;

    void Update()
    {
        CatchInput();
    }

    private void FixedUpdate()
    {
        if (bIsMoving)
        {
            bIsMoving = !MoveTowards(DesiredPosition);
        }
    }

    void CatchInput()
    {
        if (Input.GetButtonDown("Move"))
        {
            DesiredPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DesiredPosition.z = 0.0f;
            bIsMoving = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (bIsVulnerable)
            { 
                TakeDamage(1); //tenemos un solo enemigo y siempre mete 1 de daño, me tomo la libertad de magic numberearlo
                StartCoroutine(InvulnerabilityFrames());
            }
        }
    }

    protected override void Die()
    {
        Debug.Log("GAME OVER, TE MORISTE");
    }

    IEnumerator InvulnerabilityFrames()
    {
        bIsVulnerable = false;
        yield return new WaitForSeconds(InvulnerabilityFramesTime);
        bIsVulnerable = true;
    }
}
