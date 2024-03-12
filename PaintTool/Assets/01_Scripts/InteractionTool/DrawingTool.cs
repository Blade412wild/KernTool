using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawingTool : MonoBehaviour
{
    public Material material;
    private MaterialPropertyBlock block;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        block = new MaterialPropertyBlock();

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CheckIfMouseClickIsEnemy();
        }
    }
    public void CheckIfMouseClickIsEnemy()
    {
        Debug.Log("clicked");
        Vector3 mousePos = Input.mousePosition;
        Ray ray = camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Pixel pixel = hitInfo.collider.gameObject.GetComponent<Pixel>();
            if (pixel != null)
            {
                Debug.Log(pixel.transform.position);
            }
        }
        else
        {
            Debug.Log("didnt hit");
        }
    }
}

