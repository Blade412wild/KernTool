using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolColor : MonoBehaviour
{
    public Color color;
    private Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        image.color = color;
    }
}
