using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawingTool : MonoBehaviour
{
    public static System.Action<Color> OnColorChange;
    private Color brushColor = Color.black;

    private void Start()
    {
        OnColorChange += changeColor;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            draw();
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
            CanvasTexture texutre = hitInfo.collider.gameObject.GetComponent<CanvasTexture>();
            if (texutre != null)
            {
                texutre.texture.SetPixel((int)texturePos.x, (int)texturePos.y, brushColor);
                texutre.texture.Apply();
                Debug.Log(texutre.transform.position);
            }
        }
    }

    private void changeColor(Color _color)
    {
        brushColor = _color;
    }


}

