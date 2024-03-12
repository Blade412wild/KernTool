// Sets the y coordinate of the transform to follow a heightmap

using UnityEngine;
using System.Collections;

public class GetPixel : MonoBehaviour
{
    public Texture2D heightmap;
    public Vector3 size = new Vector3(100, 10, 100);

    void Update()
    {
        int x = Mathf.FloorToInt(transform.position.x / size.x * heightmap.width);
        int z = Mathf.FloorToInt(transform.position.z / size.z * heightmap.height);
        Vector3 pos = transform.position;
        pos.y = heightmap.GetPixel(x, z).grayscale * size.y;
        transform.position = pos;
    }
}