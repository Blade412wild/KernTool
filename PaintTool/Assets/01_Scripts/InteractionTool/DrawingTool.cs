using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawingTool : MonoBehaviour
{
    public static System.Action<Color> OnColorChange;
    [SerializeField] private CanvasTexture texutre;
    private Color brushColor = Color.black;

    private Vector3 previousMousWorldPos;
    private Vector3 currentMousWorldPos;

    private int pathCount;
    private bool releasedBrush = true;

    private int yPathCount;
    private int xPathCount;


    private void Start()
    {
        OnColorChange += changeColor;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //draw();
            CalculateBrush();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetTool();
        }




    }

    private void OnDisable()
    {
        OnColorChange -= changeColor;

    }
    public void draw()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 texturePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (texutre != null)
            {
                texutre.texture.SetPixel((int)texturePos.x, (int)texturePos.y, brushColor); // dit moet ik nog refactoren 
                texutre.texture.Apply();
                Debug.Log(texutre.transform.position);
            }
        }
    }

    private void changeColor(Color _color)
    {
        brushColor = _color;
    }

    private void CalculateBrush()
    {
        Vector3 mousePos = Input.mousePosition;
        currentMousWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (releasedBrush == true)
        {
            previousMousWorldPos = currentMousWorldPos;
        }

        if (currentMousWorldPos != previousMousWorldPos)
        {
            DicidePath();
        }
        else
        {
            DrawPixel(currentMousWorldPos);
        }



        previousMousWorldPos = currentMousWorldPos;
        releasedBrush = false;
    }

    private void DicidePath()
    {
        float xLenght = currentMousWorldPos.x - previousMousWorldPos.x;
        float yLenght = currentMousWorldPos.y - previousMousWorldPos.y;

        int yPathCount = (int)yLenght;
        int xPathCount = (int)xLenght;

        if (yPathCount < 0)
        {
            yPathCount = yPathCount * -1;
        }

        if (xPathCount < 0)
        {
            xPathCount = xPathCount * -1;
        }


        if (xLenght > 0 && yLenght > 0)
        {
            CalculatePath(xPathCount, yPathCount, 1, 1);
        }

        if (xLenght > 0 && yLenght < 0)
        {
            CalculatePath(xPathCount, yPathCount, 1, -1);

        }

        if (xLenght < 0 && yLenght > 0)
        {
            CalculatePath(xPathCount, yPathCount, -1, 1);

        }

        if (xLenght < 0 && yLenght < 0)
        {
            CalculatePath(xPathCount, yPathCount, -1, -1);

        }

    }

    private void CalculatePath(int _xPathCount, int _yPathCount, int _xDirection, int _yDirection)
    {

        // draws xAxis first 
        for (int i = 0; i < _xPathCount; i++)
        {
            //Debug.Log("xpath : " + _xPathCount);
            previousMousWorldPos.x = previousMousWorldPos.x + _xDirection;
            DrawPixel(previousMousWorldPos);
        }

        // draws yAxis After
        for (int j = 0; j < _yPathCount; j++)
        {
            //Debug.Log("ypath : " + _yPathCount);
            previousMousWorldPos.y = previousMousWorldPos.y + _yDirection;
            DrawPixel(previousMousWorldPos);
        }

        texutre.texture.Apply();
    }

    private void DrawPixel(Vector3 _pixelPos)
    {
        texutre.texture.SetPixel((int)_pixelPos.x, (int)_pixelPos.y, brushColor);
        //texutre.texture.Apply();
        //Debug.Log(texutre.transform.position);
    }

    private void ResetTool()
    {
        releasedBrush = true;
        xPathCount = 0;
        yPathCount = 0;
        Debug.Log("released brush : " + releasedBrush);
    }


}

