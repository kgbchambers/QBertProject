using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlickSam : Ball
{
    private Vector3 toCheck;
    string blockKey;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.AddPoints(300);
            Destroy(this.gameObject);
        }
    }


    protected override IEnumerator BounceBall()
    {
        float time = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = Vector3.zero;

        //get direction to bounce randomly between 2 points
        direction = Random.Range(0, 2);

        //wait for time ball is bouncing, then set bouncing to false and move ball depending on direction
       
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

        toCheck = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        blockKey = toCheck.ToString();
        GameManager.Instance.CheckEnemyBlock(blockKey);

        //if ball move below stage blocks, Destroy it
        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }


        yield return new WaitForSeconds(ballSpeed);
        isBouncing = false;
    }
}
