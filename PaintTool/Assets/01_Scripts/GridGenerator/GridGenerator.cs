using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Define the size of the 2D array
    [SerializeField] private int height = 3;
    [SerializeField] private int width = 3;
    [SerializeField] private int layer = 3;



    // Declare the 2D array
    private int[,] myArray;

    void Start()
    {
        // Initialize the 2D array with the defined size
        myArray = new int[height, width];

        // Populate the 2D array with some values
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                myArray[i, j] = i * width + j;
            }
        }

        // Access and print the values of the 2D array
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Debug.Log("myArray[" + i + "," + j + "] = " + myArray[i, j]);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(i , layer, j );

            }
        }
    }

}
