using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBall : Ball
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.LoseLife();
    }
}
