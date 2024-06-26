using UnityEngine;

public class DrawingTool : MonoBehaviour
{
    public static System.Action<Color> OnColorChange;
    public static System.Action<DrawingCanvas> OnInputRelease;
    public static System.Action OnInput;

    [SerializeField] private DrawingCanvas texture;
    private Color brushColor = Color.black;

    private Vector3 previousMousWorldPos;
    private Vector3 currentMousWorldPos;

    private int pathCount;
    private bool releasedBrush = true;

    private int yPathCount;
    private int xPathCount;

    private bool mayDraw = true;

    private void Start()
    {
        OnColorChange += changeColor;
        UIDetector.OnUIHover += MayDraw;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (mayDraw == false) return;
            OnInput?.Invoke();
            CalculateBrush();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (mayDraw == false) return;
            ResetTool();
            OnInputRelease?.Invoke(texture);
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
            if (texture != null)
            {
                texture.texture.SetPixel((int)texturePos.x, (int)texturePos.y, brushColor); 
                texture.texture.Apply();
            }
        }
    }

    private void changeColor(Color _color)
    {
        brushColor = _color;
    }

    private void CalculateBrush()
    {
        Vector3 mousePos = Input.mousePosition;
        currentMousWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (releasedBrush == true)
        {
            previousMousWorldPos = currentMousWorldPos;
        }

        if (currentMousWorldPos != previousMousWorldPos)
        {
            DicidePath();
        }
        else
        {
            DrawPixel(currentMousWorldPos);
        }

        previousMousWorldPos = currentMousWorldPos;
        releasedBrush = false;
    }

    private void DicidePath()
    {
        float xLenght = currentMousWorldPos.x - previousMousWorldPos.x;
        float yLenght = currentMousWorldPos.y - previousMousWorldPos.y;

        yPathCount = (int)yLenght;
        xPathCount = (int)xLenght;

        if (yPathCount < 0)
        {
            yPathCount = yPathCount * -1;
        }

        if (xPathCount < 0)
        {
            xPathCount = xPathCount * -1;
        }


        if (xLenght > 0 && yLenght > 0)
        {
            CalculatePath(xPathCount, yPathCount, 1, 1);
        }

        if (xLenght > 0 && yLenght < 0)
        {
            CalculatePath(xPathCount, yPathCount, 1, -1);

        }

        if (xLenght < 0 && yLenght > 0)
        {
            CalculatePath(xPathCount, yPathCount, -1, 1);

        }

        if (xLenght < 0 && yLenght < 0)
        {
            CalculatePath(xPathCount, yPathCount, -1, -1);

        }

    }

    private void CalculatePath(int _xPathCount, int _yPathCount, int _xDirection, int _yDirection)
    {

        // draws xAxis first 
        for (int i = 0; i < _xPathCount; i++)
        {
            previousMousWorldPos.x = previousMousWorldPos.x + _xDirection;
            DrawPixel(previousMousWorldPos);
        }

        // draws yAxis After
        for (int j = 0; j < _yPathCount; j++)
        {
            previousMousWorldPos.y = previousMousWorldPos.y + _yDirection;
            DrawPixel(previousMousWorldPos);
        }

        texture.texture.Apply();
    }

    private void DrawPixel(Vector3 _pixelPos)
    {
        texture.texture.SetPixel((int)_pixelPos.x, (int)_pixelPos.y, brushColor);
    }

    private void ResetTool()
    {
        releasedBrush = true;
        xPathCount = 0;
        yPathCount = 0;
    }


    private void MayDraw(bool _result)
    {
        mayDraw = _result;
    }
}

