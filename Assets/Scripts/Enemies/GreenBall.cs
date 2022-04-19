using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : Ball
{
     public void OnTriggerEnter(Collider other)
     {
        if (other.tag == "Player")
        {
            GameManager.Instance.AddPoints(100);
            Destroy(this.gameObject);
        }
     }

}
