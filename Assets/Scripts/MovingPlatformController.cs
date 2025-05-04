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
    [SerializeField] private Transform endPoint1;
    [SerializeField] private Transform endPoint2;
    private bool towardsEnd1 =  true;
    [SerializeField] private GameObject platformBounds;


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
                    if (end1Distance > 3f)
                    {
                        myRB.AddForce(end1Dir * 250f, ForceMode2D.Force);
                        if (playerRB != null)
                        {
                            playerRB.AddForce(end1Dir * 250f, ForceMode2D.Force);
                        }
                        else
                        {
                            myRB.AddForce(end1Dir * 400f, ForceMode2D.Force); //if player isn't on it we add extra force
                        }
                    }
                    else
                    {
                        myRB.AddForce(end1Dir * 200f, ForceMode2D.Force);
                        if (playerRB != null)
                        {
                            playerRB.AddForce(end1Dir * 200f, ForceMode2D.Force);
                        }
                    }
                }
                else
                {
                    StartCoroutine(PauseThenContinue());
                    towardsEnd1 = false;
                }
            }
            else
            {
                float end2Distance = Vector3.Distance(transform.position, endPoint2.position);
                if (end2Distance > 0.1f)
                {
                    Vector3 end2Dir = (endPoint2.position - transform.position).normalized;
                    
                    if (end2Distance > 3f)
                    {
                        myRB.AddForce(end2Dir * 250f, ForceMode2D.Force);
                        if (playerRB != null)
                        {
                            playerRB.AddForce(end2Dir * 250f, ForceMode2D.Force);
                        }
                        else
                        {
                            myRB.AddForce(end2Dir * 400f, ForceMode2D.Force); //if player isn't on it we add extra force
                        }
                    }
                    else //it is close so slow down
                    {
                        myRB.AddForce(end2Dir * 200f, ForceMode2D.Force);
                        if (playerRB != null)
                        {
                            playerRB.AddForce(end2Dir * 200f, ForceMode2D.Force);
                        }
                    }
                }
                else
                {
                    StartCoroutine(PauseThenContinue());
                    towardsEnd1 = true;
                }
            }
        }

    }

    private IEnumerator PauseThenContinue()
    {
        myRB.velocity = Vector3.zero;
        if (playerRB != null)
        {
            playerRB.velocity = Vector3.zero;
        }
        
        movement = false;
        yield return new WaitForSeconds(2f);
        movement = true;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "OracleHitBox")
        {
            playerRB = Utils.Utils.Utils.FindClosestRootWithSprite(other.gameObject).GetComponent<Rigidbody2D>();
            platformBounds.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "OracleHitBox")
        {
            playerRB = null;
            platformBounds.SetActive(true);
        }
    }
}
