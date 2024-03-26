using UnityEngine;
using System.Collections;

public class CanvasTexture : MonoBehaviour
{
    public Texture2D texture;
    void Start()
    {
        SaveCheck.OnNewProjectButtonClicked+= NewTexture;

        NewTexture();
    }

    public void NewTexture()
    {
        Debug.Log(" new project call");
        texture = new Texture2D(128, 128)
        {
            filterMode = FilterMode.Point
        };
        texture.name = "textureTest";

        GetComponent<Renderer>().material.mainTexture = texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = Color.white;
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }


}