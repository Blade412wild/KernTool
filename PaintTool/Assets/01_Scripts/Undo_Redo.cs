using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using UnityEngine;

public class Undo_Redo : MonoBehaviour
{
    [SerializeField] private CanvasTexture mainCanvas;

    [SerializeField] private Stack<byte[]> redoStack = new Stack<byte[]>();
    [SerializeField] private Stack<byte[]> undoStack = new Stack<byte[]>();

    private bool firstUndo = true;
    private bool firstRedo = true;


    // Start is called before the first frame update
    void Start()
    {
        DrawingTool.OnInputRelease += AddToUndo;
        DrawingTool.OnInput += SetFirstUndoTrue;
        //mainCanvas.texture.SetPixel(0, 0, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCanvas != null)
        {
            mainCanvas.texture.SetPixel(10, 10, UnityEngine.Color.green);
            mainCanvas.texture.Apply();
        }

        //if (Input.GetKey(KeyCode.LeftControl))
        //{
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (undoStack.Count == 0) return;

            if (firstUndo == true)
            {
                redoStack.Push(undoStack.Pop());
                firstUndo = false;
                firstRedo = true;
            }

            var previousTexture = undoStack.Pop();
            Load(previousTexture);
            redoStack.Push(previousTexture);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (redoStack.Count == 0) return;

            if (firstRedo == true)
            {
                undoStack.Push(redoStack.Pop());
                firstUndo = true;
                firstRedo = false;
            }

            var previousTexture = redoStack.Pop();
            Load(previousTexture);
            undoStack.Push(previousTexture);
        }

        //}
    }

    private void AddToRedo(CanvasTexture _canvasTexture)
    {

    }

    private void AddToUndo(CanvasTexture _canvasTexture)
    {
        byte[] quicksave = EncodeTexToPNG();
        undoStack.Push(quicksave);
        Debug.Log("add Texture to Undo");
    }

    private byte[] EncodeTexToPNG()
    {
        Texture2D tex = new Texture2D(128, 128)
        {
            filterMode = FilterMode.Point
        };
        tex.name = "quickesave 1";
        tex = mainCanvas.texture;
        Debug.Log("attempting to encode " + tex.name + " to .png");
        byte[] bytes = ImageConversion.EncodeToPNG(tex);

        Debug.Log("encoded " + tex.name + " to .png");
        return bytes;
    }

    private void Load(byte[] _encodedPNG)
    {
        ImageConversion.LoadImage(mainCanvas.texture, _encodedPNG);
    }

    private void SetFirstUndoTrue()
    {
        firstUndo = true;
        firstRedo = true;

    }

}
