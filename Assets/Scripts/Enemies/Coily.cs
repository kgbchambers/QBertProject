using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coily : MonoBehaviour
{
    public float speed;
    private int choice;
    private Vector3 targetPosition;

    void Start()
    {
        StartCoroutine(TargetPlayer());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.LoseLife();
            Destroy(this.gameObject);
        }
        else if(other.tag != "Cube" && other.tag != "Player")
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator TargetPlayer()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            targetPosition = PlayerController.Instance.transform.position;
            //Debug.Log(targetPosition);

            if(targetPosition.y > transform.position.y && targetPosition.x == transform.position.x)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
            }
            else if(targetPosition.y < transform.position.y && targetPosition.x == transform.position.x)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - 1);
            }
            else if(targetPosition.y > transform.position.y && targetPosition.z == transform.position.z)
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
            }
            else if (targetPosition.y < transform.position.y && targetPosition.z == transform.position.z)
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
            }
            else if(targetPosition.y > transform.position.y && targetPosition.x < transform.position.x)
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
            }
            else if (targetPosition.y < transform.position.y && targetPosition.x > transform.position.x)
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
            }
            else if (targetPosition.y > transform.position.y && targetPosition.x > transform.position.x)
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
            }
            else if (targetPosition.y < transform.position.y && targetPosition.z < transform.position.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - 1);
            }
            else if (targetPosition.y == transform.position.y && targetPosition.z < transform.position.z)
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
            }
            else if (targetPosition.y == transform.position.y && targetPosition.z > transform.position.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
            }
            yield return new WaitForSeconds(speed);

        }
    }
}
