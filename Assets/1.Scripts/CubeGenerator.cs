using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public GameObject cubePrefabs;
    public int totalCube = 10;
    public float cubeSpacing = 1.0f;


    private void Start()
    {
        GenCube();
    }

    public void GenCube()
    {
        Vector3 myPosition = transform.position;

        GameObject firstCube = Instantiate(cubePrefabs,myPosition,Quaternion.identity);

        for (int i = 1; i < totalCube; i++)
        {
            Vector3 position = new Vector3(myPosition.x, myPosition.y,myPosition.z + (i * cubeSpacing));
            Instantiate(cubePrefabs,position,Quaternion.identity);
        }
    }
}
