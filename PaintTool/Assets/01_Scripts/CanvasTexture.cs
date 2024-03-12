using UnityEngine;
using System.Collections;

public class CanvasTexture : MonoBehaviour
{
    public Texture2D texture;
    void Start()
    {
        texture = new Texture2D(128, 128)
        {
            filterMode = FilterMode.Point
        };

        GetComponent<Renderer>().material.mainTexture = texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = Color.white;
                texture.SetPixel(x, y, color);
            }
        }
        texture.SetPixel(0,0, Color.red);
        texture.Apply();


        
    }
}