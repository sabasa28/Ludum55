using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckerCatProjectile : MonoBehaviour
{
    public Vector3 TargetDirection;
    [SerializeField]
    float MovementSpeed;
    [SerializeReference]
    float TavelTime;
    Animator Anim;
    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(MoveInDirection());   
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    void ShowDeathSprite()
    {
        Anim.SetTrigger("Death");
    }

    IEnumerator MoveInDirection()
    {
        float angle = Mathf.Atan2(TargetDirection.y, TargetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float Timer = 0.0f;
        while (Timer < TavelTime)
        {
            Timer += Time.deltaTime;
            transform.position += TargetDirection.normalized * MovementSpeed * Time.deltaTime;
            yield return null;
        }
        ShowDeathSprite();
    }
}
