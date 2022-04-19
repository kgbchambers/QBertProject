using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilyBall : Ball
{
    public GameObject Coily;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.LoseLife();
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

        //if ball move below stage blocks, Destroy it
        if (transform.position.y == -5)
        {
            EnemyManager.Instance.SpawnCoily(Coily, transform.position);
            Destroy(this.gameObject);
        }
        yield return new WaitForSeconds(ballSpeed);
        isBouncing = false;

    }

}
