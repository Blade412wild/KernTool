using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawingTool : MonoBehaviour
{

    private void Start()
    {
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            CheckIfMouseClickIsEnemy();
        }
    }
    public void CheckIfMouseClickIsEnemy()
    {
        Debug.Log("clicked");
        Vector3 mousePos = Input.mousePosition;
        Vector3 texturePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.Log("Tmouse pos : [" + texturePos.x + ", " + texturePos.y + "] ");

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            CanvasTexture exmapleClass = hitInfo.collider.gameObject.GetComponent<CanvasTexture>();
            if (exmapleClass != null)
            {
                exmapleClass.texture.SetPixel((int)texturePos.x, (int)texturePos.y, Color.red);
                //exmapleClass.texture.SetPixel(10,10, Color.blue);
                exmapleClass.texture.Apply();
                Debug.Log(exmapleClass.transform.position);
            }
        }
    }
}

