using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public static PlayerController playerControllerInstance;
    
    private PlayerInput playerInputActions;
    private float speed;
    private Vector3 direction;

    [SerializeField]
    private bool canMove;

    private float xCount = 0f;
    private float yCount = 1f;
    private float zCount = 0f;


    private void Start()
    {
        playerInputActions = new PlayerInput();
        playerInputActions.Enable();
        canMove = true;
    }


    private void Update()
    {
        direction = playerInputActions.Player.Movement.ReadValue<Vector2>();
    }



    //use bezier interpolation
    public void Move(InputAction.CallbackContext context)
    {
        if(direction.x == 1 && canMove)
        {
            transform.position = new Vector3(xCount + 1, yCount - 1, zCount);
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            xCount++;
            yCount--;
            if(xCount > 6 || yCount < -6)
            {
                Debug.Log("Kill Player");
            }
        }
        else if(direction.x == -1 && canMove)
        {
            transform.position = new Vector3(xCount - 1, yCount + 1, zCount);
            this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            xCount--;
            yCount++;
            if (yCount > 1 || xCount < 0)
            {
                Debug.Log("Kill Player");
            }
        }
        else if (direction.y == 1 && canMove)
        {
            transform.position = new Vector3(xCount, yCount + 1, zCount + 1);
            this.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

            yCount++;
            zCount++;
            if (yCount > 1 || zCount > 0)
            {
                Debug.Log("Kill Player");
            }

        }
        else if(direction.y == -1 && canMove)
        {
            transform.position = new Vector3(xCount, yCount - 1, zCount - 1);
            this.transform.rotation = Quaternion.Euler(0f,90f,0f);
            yCount--;
            zCount--;
            if (yCount < -5 || zCount < -6)
            {
                Debug.Log("Kill Player");
            }
        }
    }


    IEnumerator MoveDelay()
    {
        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
    }
}










