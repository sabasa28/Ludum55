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
            MoveTowards();
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

    void MoveTowards()
    {
        if (Vector3.Distance(transform.position, DesiredPosition) < MovementSpeed * Time.fixedDeltaTime)
        {
            transform.position = DesiredPosition;
            bIsMoving = false;
            return;
        }

        Vector3 Direction = (DesiredPosition - transform.position).normalized;
        transform.position = transform.position + Direction * MovementSpeed * Time.fixedDeltaTime;
    }


}
