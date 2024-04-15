using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkyCatAOE : MonoBehaviour
{
    CircleCollider2D TriggerForDamage;
    SpriteRenderer SpriteRend;
    private void Awake()
    {
        TriggerForDamage = GetComponent<CircleCollider2D>();
        SpriteRend = GetComponentInChildren<SpriteRenderer>();
    }
    public void ActivateDamage()
    {
        TriggerForDamage.enabled = true;
        if (SpriteRend)
        { 
            SpriteRend.enabled = true;
        }
    }
    public void DeactivateDamage()
    {
        TriggerForDamage.enabled = false;
        if (SpriteRend)
        {
            SpriteRend.enabled = false;
        }
    }
}
