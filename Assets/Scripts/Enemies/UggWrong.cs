using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UggWrong : MonoBehaviour
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
                spawnPoint = new Vector3(0, -6, -7);
                break;
            case 1:
                spawnPoint = new Vector3(7, -6, 0);
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        transform.position = spawnPoint;

    }


    private void Update()
    {
        if (!isBouncing)
        {
            isBouncing = true;
            StartCoroutine(BounceBall());
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameManager.Instance.LoseLife();
    }


    IEnumerator BounceBall()
    {
        float time = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = Vector3.zero;


        //get direction to bounce randomly between 2 points
        direction = Random.Range(0, 2);

        //wait for time ball is bouncing, then set bouncing to false and move ball depending on direction
        switch (spawnDirection)
        {
            case 0:
                if(direction == 0)
                {
                    targetPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z + 1);
                }
                else if(direction == 1)
                {
                    targetPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
                }
                break;
            case 1:
                if(direction == 0)
                {
                    targetPos = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
                }
                else if(direction == 1)
                {
                    targetPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z - 1);
                }
                break;
        }

        while (time < ballSpeed)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / ballSpeed);
            time += Time.deltaTime;
            yield return null;
        }

       
        //if ball move below stage blocks, Destroy it
        if (spawnDirection == 0 && transform.position.z > 0)
            Destroy(this.gameObject);
        else if(spawnDirection == 1 && transform.position.x < 0)
            Destroy(this.gameObject);


        yield return new WaitForSeconds(ballSpeed);
        isBouncing = false;
    }
}
