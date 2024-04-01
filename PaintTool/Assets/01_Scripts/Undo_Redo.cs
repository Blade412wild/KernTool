using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using UnityEngine;

public class Undo_Redo : MonoBehaviour
{
    [SerializeField] private DrawingCanvas mainCanvas;
    [SerializeField] private DrawingCanvas miniCanvas;

    private Stack<byte[]> redoStack = new Stack<byte[]>();
    private Stack<byte[]> undoStack = new Stack<byte[]>();

    // Start is called before the first frame update
    void Start()
    {
        DrawingTool.OnInputRelease += AddToUndo;
        DrawingTool.OnInput += EmptyRedo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Undo();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Redo();
        }
    }

    private void Redo()
    {
        if (redoStack.Count == 0) return;
        byte[] previousTexture = redoStack.Pop();
        Load(mainCanvas, previousTexture);
        undoStack.Push(previousTexture);
    }

    private void Undo()
    {
        if (undoStack.Count == 0)
        {
            mainCanvas.NewTexture();
        }
        else
        {
            redoStack.Push(undoStack.Pop());

            if (undoStack.Count == 0)
            {
                mainCanvas.NewTexture();
            }
            else
            {
                byte[] previousTexture = undoStack.Peek();
                Load(mainCanvas, previousTexture);
            }
        }
    }

    private void AddToUndo(DrawingCanvas _canvasTexture)
    {
        byte[] quicksave = EncodeTexToPNG();
        undoStack.Push(quicksave);
    }

    private byte[] EncodeTexToPNG()
    {
        Texture2D tex = new Texture2D(128, 128)
        {
            filterMode = FilterMode.Point
        };
        tex.name = "quickesave 1";
        tex = mainCanvas.texture;
        byte[] bytes = ImageConversion.EncodeToPNG(tex);
        return bytes;
    }

    private void Load(DrawingCanvas canvas, byte[] _encodedPNG)
    {
        ImageConversion.LoadImage(canvas.texture, _encodedPNG);
    }

    private void EmptyRedo()
    {
        redoStack.Clear();
    }
}
