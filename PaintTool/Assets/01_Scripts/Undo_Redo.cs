using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using UnityEngine;

public class Undo_Redo : MonoBehaviour
{
    [SerializeField] private CanvasTexture mainCanvas;
    [SerializeField] private CanvasTexture miniCanvas;


    [SerializeField] private Stack<byte[]> redoStack = new Stack<byte[]>();
    [SerializeField] private Stack<byte[]> undoStack = new Stack<byte[]>();

    private bool firstUndo = true;
    private bool firstRedo = true;

    private Texture2D emptyTexture;
    private byte[] encodedEmptyTexture;

    // Start is called before the first frame update
    void Start()
    {
        DrawingTool.OnInputRelease += AddToUndo;
        DrawingTool.OnInput += SetFirstUndoTrue;
        DrawingTool.OnInput += EmptyRedo;
        //byte[] encodedEmptyTexture = ImageConversion.EncodeToPNG(emptyTexture);

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("firstUndo : " + firstUndo);
        Debug.Log("firstRedo : " + firstRedo);

        Debug.Log("UndoStack : " + undoStack.Count + " | " + "RedoStack : " + redoStack.Count);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Undo2();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Redo();
        }
    }

    private void Redo()
    {
        if (redoStack.Count == 0) return;

        if (undoStack.Count == 0)
        {
            byte[] previousTexture = redoStack.Pop();
            Load(mainCanvas, previousTexture);
            undoStack.Push(previousTexture);
            firstUndo = true;
            firstRedo = false;
        }
        else
        {
            if (firstRedo == true)
            {
                undoStack.Push(redoStack.Pop());
                firstUndo = true;
                firstRedo = false;
            }

            if (undoStack.Count == 0) return;
            byte[] previousTexture = redoStack.Pop();
            Load(miniCanvas, previousTexture);
            Load(mainCanvas, previousTexture);
            undoStack.Push(previousTexture);
        }
    }



    private void Undo()
    {
        Debug.Log("undo");
        if (undoStack.Count == 0)
        {
            mainCanvas.NewTexture();
        }
        else
        {
            if (redoStack.Count == 0)
            {
                Debug.Log(" einde redo");
                byte[] previousTexture = undoStack.Pop();
                Load(miniCanvas, previousTexture);
                Load(mainCanvas, previousTexture);
                redoStack.Push(previousTexture);
                firstUndo = true;
                firstRedo = false;

            }
            else
            {
                if (firstUndo == true)
                {
                    redoStack.Push(undoStack.Pop());
                    firstUndo = false;
                    firstRedo = true;
                }

                byte[] previousTexture = undoStack.Pop();
                Load(miniCanvas, previousTexture);
                Load(mainCanvas, previousTexture);
                redoStack.Push(previousTexture);

            }



        }
    }

    private void Undo2()
    {
        if (undoStack.Count == 0)
        {
            mainCanvas.NewTexture();
        }
        else
        {

            if (firstUndo == true)
            {
                redoStack.Push(undoStack.Pop());
                firstUndo = false;
                firstRedo = true;
            }

            if (undoStack.Count == 0)
            {
                mainCanvas.NewTexture();
            }
            else
            {
                byte[] previousTexture = undoStack.Pop();
                Load(miniCanvas, previousTexture);
                Load(mainCanvas, previousTexture);
                redoStack.Push(previousTexture);
            }
        }
    }

    private void AddToUndo(CanvasTexture _canvasTexture)
    {
        byte[] quicksave = EncodeTexToPNG();

        if (redoStack.Count > 0 && firstUndo == false)
        {
            undoStack.Push(redoStack.Pop());
            undoStack.Push(redoStack.Pop());
        }

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

    private void Load(CanvasTexture canvas, byte[] _encodedPNG)
    {
        ImageConversion.LoadImage(canvas.texture, _encodedPNG);
    }

    private void SetFirstUndoTrue()
    {
        firstUndo = true;
        firstRedo = true;
    }
    private void EmptyRedo()
    {
        redoStack.Clear();
    }


}
