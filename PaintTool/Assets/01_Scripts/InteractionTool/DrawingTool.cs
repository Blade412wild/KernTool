using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawingTool : MonoBehaviour
{
    public static System.Action<Color> OnColorChange;
    [SerializeField] private CanvasTexture texutre;
    private Color brushColor = Color.black;

    private Vector3 previousMousWorldPos;
    private Vector3 currentMousWorldPos;

    private int pathCount;


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

        if (previousMousWorldPos == null)
        {
            previousMousWorldPos = currentMousWorldPos;
        }

        if (currentMousWorldPos != previousMousWorldPos)
        {
            DicidePath();
        }

        previousMousWorldPos = currentMousWorldPos;
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

        if(xPathCount < 0)
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
            Debug.Log("xpath : " + _xPathCount);
            previousMousWorldPos.x = previousMousWorldPos.x + _xDirection;
            drawPixel(previousMousWorldPos);
        }

        // draws yAxis After
        for (int j = 0; j < _yPathCount; j++)
        {
            Debug.Log("ypath : " + _yPathCount);
            previousMousWorldPos.y = previousMousWorldPos.y + _yDirection;
            drawPixel(previousMousWorldPos);
        }
    }

    private void drawPixel(Vector3 _pixelPos)
    {
        texutre.texture.SetPixel((int)_pixelPos.x, (int)_pixelPos.y, brushColor); 
        texutre.texture.Apply();
        Debug.Log(texutre.transform.position);
    }


}

