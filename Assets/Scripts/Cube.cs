using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int value;
    public int curLevel;
    public GameObject plane;
    public List<Material> materialList = new List<Material>();



    public void Awake()
    {
        value = 0;
        curLevel = 0;
        plane.GetComponent<MeshRenderer>().material = materialList[value];
    }

    public void PlayerCubeUpdate()
    {
        if (curLevel < 3)
        {
            if (value < curLevel)
            {
                value++;
                plane.GetComponent<MeshRenderer>().material = materialList[value];
                GameManager.Instance.AddPoints(25);
                return;
            }
        }
        else if(curLevel >= 3)
        {
            if (value >= 3)
                value = 1;
            else
                value++;
            plane.GetComponent<MeshRenderer>().material = materialList[value];
            GameManager.Instance.AddPoints(25);
        }
    }

    public void EnemyCubeUpdate()
    {
        if(value >= 1  && curLevel < 3)
        {
            value--;
            plane.GetComponent<MeshRenderer>().material = materialList[value];
        }
        else if(curLevel >= 3)
        {
            value--;
            if(value < 0)
            {
                value = 3;
            }
            plane.GetComponent<MeshRenderer>().material = materialList[value];
        }
    }


    public void NextLevel()
    {
        curLevel++;
        value = 0;
        plane.GetComponent<MeshRenderer>().material = materialList[value];
    }

    public void ResetGame()
    {
        curLevel = 1;
        value = 0;
        plane.GetComponent<MeshRenderer>().material = materialList[value];
    }


    public void TargetCubeChange()
    {
        curLevel++;
        value++;
        if (value > 3 || value < 1)
            value = 1;
        plane.GetComponent<MeshRenderer>().material = materialList[value];

    }

    public void TargetCubeReset()
    {

        curLevel = 1;
        value = 1;
        plane.GetComponent<MeshRenderer>().material = materialList[value];
    }

}
