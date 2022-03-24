using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed;
    public bool isBouncing;
    protected int direction;
    private void Start()
    {
        isBouncing = false;    
    }


    //check if ball is bouncing: if not, set to true and start bouncing to next level of blocks
    private void Update()
    {
        if (!isBouncing)
        {
            isBouncing = true;
            StartCoroutine(BounceBall());
        }
    }


    //handle ball bouncing between multiple frames with CoRoutine
    IEnumerator BounceBall()
    {
        //get direction to bounce randomly between 2 points
        direction = Random.Range(0, 2);

        //wait for time ball is bouncing, then set bouncing to false and move ball depending on direction
        yield return new WaitForSeconds(ballSpeed);
        isBouncing = false;
        switch (direction)
        {
            case 0:
                transform.position = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - 1);
                break;
            default:
                break;
        }

        //if ball move below stage blocks, Destroy it
        if(transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
        yield return null;
    }
}
