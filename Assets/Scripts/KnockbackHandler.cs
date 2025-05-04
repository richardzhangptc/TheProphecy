using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackHandler : MonoBehaviour
{
    private Rigidbody2D myRB;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockbackToSelf(Vector2 dir, int knockbackTicks, float knockbackForce)
    {
        StartCoroutine(KnockbackCoroutine(dir, knockbackTicks, knockbackForce));
    }
    
    private IEnumerator KnockbackCoroutine(Vector2 dir, int knockbackTicks, float knockbackForce)
    {
        while(knockbackTicks > 0)
        {
            myRB.AddForce(dir * knockbackForce, ForceMode2D.Force);
            knockbackTicks--;
            yield return new WaitForFixedUpdate();
        }
    }   
    
}