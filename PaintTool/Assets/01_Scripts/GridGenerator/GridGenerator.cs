using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public static event Action OngridIsDone;

    // Define the size of the 2D array
    [SerializeField] public int width = 3;
    [SerializeField] public int height = 3;
    [SerializeField] public int layer = 3;

    [SerializeField] private GameObject pixelPrefab;



    // Declare the 2D array
    private int[,] myArray;

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        // Initialize the 2D array with the defined size
        myArray = new int[width, height];

        // Populate the 2D array with some values
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                myArray[i, j] = i * height + j;
            }
        }

        // Access and print the values of the 2D array
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Debug.Log("myArray[" + i + "," + j + "] = " + myArray[i, j]);
                GameObject cube = Instantiate(pixelPrefab);
                cube.transform.position = new Vector3(i, layer, j);
            }
        }
    }

}
