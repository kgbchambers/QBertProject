using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : Singleton<PlayerController>
{
    private PlayerInput playerInputActions;
    public float speed;
    public float moveDelay;
    private Vector3 direction;
    private Vector3 blockToCheck;
    private Vector3 spawnPosition = new Vector3(0,1,0);


    [SerializeField]
    private bool canMove;


    private void Start()
    {
        playerInputActions = new PlayerInput();
        playerInputActions.Enable();
        canMove = true;
    }



    //use bezier interpolation
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed && canMove)
        {
            canMove = false;
            direction = context.ReadValue<Vector2>();
            /*
            if (direction.x == 1)
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else if (direction.x == -1)
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            }
            else if (direction.y == 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
                this.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

            }
            else if (direction.y == -1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - 1);
                this.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }
            */
            StartCoroutine(MoveDelay(direction));
            
        }       
    }



    private void CheckBlock()
    {
        Vector3 blockPosition = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);
        string blockKey = blockPosition.ToString();
        GameManager.Instance.CheckBlockValue(blockKey);
    }



    public void ResetPlayer()
    {
        transform.position = spawnPosition;
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        StartCoroutine(DeathDelay());
        //StartCoroutine(MoveDelay(Vector2.zero));
    }


    IEnumerator DeathDelay()
    {
        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
        //StartCoroutine(MoveDelay(Vector2.zero));
    }


    IEnumerator MoveDelay(Vector2 position)
    {
        float time = 0;
        Vector3 startPos = transform.position;
        Vector3 targetPos = transform.position;
        
        if(position.x == 1)
        {
            targetPos = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (position.x == -1)
        {
            targetPos = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
            this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (position.y == 1)
        {
            targetPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
            this.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else if (position.y == -1)
        {
            targetPos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - 1);
            this.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else
        {
            targetPos = spawnPosition;
        }
        while (time < speed)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / speed);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        CheckBlock();
        yield return new WaitForSeconds(moveDelay);
        canMove = true;
    }

}










