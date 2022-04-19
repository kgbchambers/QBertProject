using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : MonoBehaviour
{
    public int value;
    public int curLevel;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.ResetLevel();
        }
    }

}
