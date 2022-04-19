using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Ball : MonoBehaviour
{
    public float ballSpeed;
    public bool isBouncing;
    protected int direction;
    protected int spawnDirection;
    Vector3 spawnPoint; // = new Vector3(1, 0, 0);

    private void Awake()
    {
        isBouncing = false;
        spawnDirection = Random.Range(0, 2);
        switch (spawnDirection)
        {
            case 0:
                spawnPoint = new Vector3(1, 0, 0);
                break;
            case 1:
                spawnPoint = new Vector3(0, 0, -1);
                break;
            default:
                break;
        }
    }


    private void Start()
    {
        transform.position = spawnPoint;
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
    protected virtual IEnumerator BounceBall()
    {
        float time = 0;
        //get direction to bounce randomly between 2 points
        direction = Random.Range(0, 2);

        Vector3 startPos = transform.position;
        Vector3 targetPos = transform.position;

        //wait for time ball is bouncing, then set bouncing to false and move ball depending on direction
        //yield return new WaitForSeconds(ballSpeed);


        switch (direction)
        {
            case 0:
                targetPos = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
                break;
            case 1:
                targetPos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - 1);
                break;
            default:
                break;
        }

        while (time < ballSpeed)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / ballSpeed);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;

        //if ball move below stage blocks, Destroy it
        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
        yield return new WaitForSeconds(ballSpeed);
        isBouncing = false;
    }
}
