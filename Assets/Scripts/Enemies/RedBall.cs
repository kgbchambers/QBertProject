using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : Ball
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            GameManager.Instance.LoseLife();
    }
}
