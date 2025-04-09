using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public CubeGenerator[] generatedCubes = new CubeGenerator[5];

    public float timer = 0f;
    public float interval = 3f;

    public void RandomCubeAction()
    {
        for (int i = 0; i < generatedCubes.Length; i++)
        {
            int randomNum = Random.Range(0, 2);
            if (randomNum == 1)
            {
                generatedCubes[i].GenCube();
            }
        }
    }
}
