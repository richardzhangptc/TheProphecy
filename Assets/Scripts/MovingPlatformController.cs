using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Utils;

public class MovingPlatformController : MonoBehaviour
{
    private bool movement = false;
    private Rigidbody2D myRB;
    private Rigidbody2D playerRB;
    private Rigidbody2D boxRB;
    [SerializeField] private Transform endPoint1;
    [SerializeField] private Transform endPoint2;
    private bool towardsEnd1 =  true;

    [SerializeField] private PlayerPull puller;


    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            MovementOn();
        }
        else if(Input.GetKey(KeyCode.O))
        {
            MovementOff();
        }
        
    }

    public void MovementOn()
    {
        movement = true;
    }
    public void MovementOff()
    {
        movement = false;
    }

    public bool playerOnPlatform()
    {
        if (playerRB != null)
        {
            return true;
        }

        return false;
    }

    private void FixedUpdate()
    {
        if (movement)
        {
            
            if (towardsEnd1)
            {
                float end1Distance = Vector3.Distance(transform.position, endPoint1.position);
                if (end1Distance > 0.1f)
                {
                    Vector3 end1Dir = (endPoint1.position - transform.position).normalized;
                    myRB.AddForce(end1Dir * 150, ForceMode2D.Force);
                    if (playerRB != null)
                    {
                        playerRB.AddForce(end1Dir * 150, ForceMode2D.Force);
                    }
                    if (boxRB != null)
                    {
                        boxRB.AddForce(end1Dir * 150, ForceMode2D.Force);
                    }
                }
                else
                {
                    towardsEnd1 = false;
                }
            }
            else
            {
                float end2Distance = Vector3.Distance(transform.position, endPoint2.position);
                if (end2Distance > 0.1f)
                {
                    Vector3 end2Dir = (endPoint2.position - transform.position).normalized;
                    
                    myRB.AddForce(end2Dir * 150, ForceMode2D.Force);
                    if (playerRB != null)
                    {
                        playerRB.AddForce(end2Dir * 150, ForceMode2D.Force);
                    }
                    if (boxRB != null)
                    {
                        boxRB.AddForce(end2Dir * 150, ForceMode2D.Force);
                    }
                }
                else
                {
                    towardsEnd1 = true;
                }
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Oracle" || other.gameObject.tag == "OracleHitBox")
        {
            playerRB = Utils.Utils.Utils.FindClosestRootWithSprite(other.gameObject).GetComponent<Rigidbody2D>();
        }
        
        if (other.gameObject.tag == "pullable")
        {
            boxRB = Utils.Utils.Utils.FindClosestRootWithSprite(other.gameObject).GetComponent<Rigidbody2D>();
            boxRB.gameObject.transform.position = transform.position;
            if (puller != null)
            {
                puller.ForceLetGo();
            }
            
        }
    }
    
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Oracle" || other.gameObject.tag == "OracleHitBox")
        {
            playerRB = null;
        }
        
        if (other.gameObject.tag == "pullable")
        {
            boxRB = null;
        }
    }
}
