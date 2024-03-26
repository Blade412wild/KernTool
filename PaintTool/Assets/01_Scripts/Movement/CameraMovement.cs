using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //[SerializeField] private GridGenerator gridGenerator;

    [Header("Movement")]
    [SerializeField] private float minMoveSpeed = 40;
    [SerializeField] private float maxMoveSpeed = 4;
    private float moveSpeed = 4;
    private float movementCoeffient = 1;

    [Header("Zoom")]
    [SerializeField] private float minZoomValue = 5;
    [SerializeField] private float maxZoomValue = 80;
    private float zoomValue = 70;

    [SerializeField] private float zoomSpeed = 1000;
    [SerializeField] private Vector3 beginPosition;

    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        MoveToStartPos();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CalculateMovePosition();
        CalculateZoom();
        CalculateMoveSpeed();
    }

    private void MoveToStartPos()
    {
        moveDir = beginPosition;
    }

    private Vector3 CalculateMovePosition()
    {
        GetXInput();
        GetYInput();

        return new Vector3(moveDir.x, moveDir.y, moveDir.z);
    }

    private void CalculateZoom()
    {
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        zoomValue += (scrollValue * zoomSpeed) * Time.deltaTime * -1;
        Camera.main.orthographicSize = zoomValue;

        if(zoomValue >= maxZoomValue)
        {
            zoomValue = maxZoomValue;
        }

        if(zoomValue <= minZoomValue)
        {
            zoomValue = minMoveSpeed;
        }
    }

    private float GetXInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x += -1 * moveSpeed * Time.deltaTime;
            return moveDir.x;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDir.x += 1 * moveSpeed * Time.deltaTime;
            return moveDir.x;
        }
        else
        {
            return 0;
        }

    }

    private float GetYInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveDir.y += 1 * moveSpeed * Time.deltaTime;
            return moveDir.y;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDir.y += -1 * moveSpeed * Time.deltaTime;
            return moveDir.y;
        }
        else
        {
            return 0;
        }

    }

    private void CalculateMoveSpeed()
    {
        movementCoeffient = zoomValue / maxZoomValue;
        moveSpeed = movementCoeffient * maxMoveSpeed;
    }

}
