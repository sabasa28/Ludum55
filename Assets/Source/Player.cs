using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    Vector3 DesiredPosition;
    bool bIsMoving;

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

    protected override void Die()
    {

    }
}
