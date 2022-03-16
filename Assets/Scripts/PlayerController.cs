using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : Singleton<PlayerController>
{
    public static PlayerController playerControllerInstance;
    
    private PlayerInput playerInputActions;
    private float speed;
    private Vector3 direction;

    private float xCount = 0;
    private float yCount = 1;
    private float zCount = 0;


    private void Start()
    {
        playerInputActions = new PlayerInput();
        playerInputActions.Enable();
        transform.position = new Vector3(xCount, yCount, zCount);
    }


    private void Update()
    {
        direction = playerInputActions.Player.Movement.ReadValue<Vector2>();
    }

    //use bezier interpolation

    public void Move(InputAction.CallbackContext context)
    {
            if(direction.x == 1)
            {
                transform.position = new Vector3(xCount + 1, yCount - 1, zCount);
                xCount++;
                yCount--;
                if(xCount > 6 || yCount < -5)
                {
                    Debug.Log("Kill Player");
                }
            }
            if(direction.x == -1)
            {
                transform.position = new Vector3(xCount - 1, yCount + 1, zCount);
                xCount--;
                yCount++;
                if (yCount > 6 || xCount < 0)
                {
                    Debug.Log("Kill Player");
                }
            }
            if (direction.y == 1)
            {
                transform.position = new Vector3(xCount, yCount + 1, zCount + 1);
                yCount++;
                zCount++;
                if (yCount > 1 || zCount > 0)
                {
                    Debug.Log("Kill Player");
                }
            }
            if(direction.y == -1)
            {
                transform.position = new Vector3(xCount, yCount - 1, zCount - 1);
                yCount--;
                zCount--;
                if (yCount < -5 || zCount < -6)
                {
                    Debug.Log("Kill Player");
                }
        }
    }
}










