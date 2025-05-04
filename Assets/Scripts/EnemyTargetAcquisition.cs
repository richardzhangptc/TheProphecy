using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Utils;
using Pathfinding;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyTargetAcquisition : MonoBehaviour
{
    private AIPathNew pathScript;

    private void Start()
    {
        pathScript = Utils.Utils.Utils.FindClosestRootWithSprite(gameObject).GetComponent<AIPathNew>();
    }

    private void OnTriggerStay2D(Collider2D collided) //expand on this later to check different targets and priorities
    {
        if(collided.gameObject.tag == "PlayerHitBox")
        {
            LayerMask hitMask = LayerMask.GetMask("Player");
            hitMask += LayerMask.GetMask("Objects");

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, collided.gameObject.transform.position - transform.position, 100f, hitMask); //raycast origin, direction
            if(hitInfo.collider != null && Utils.Utils.Utils.FindClosestRootWithSprite(hitInfo.collider.gameObject).tag == "Monster") //have to get the root of the hit because the cast collides only with the player's outer most collider
            {
                pathScript.target = Utils.Utils.Utils.FindClosestRootWithSprite(hitInfo.collider.gameObject).transform;
                pathScript.inCombat = true;
            }
        }
    } 



}
