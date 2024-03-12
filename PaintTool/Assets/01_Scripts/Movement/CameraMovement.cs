using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float zoomSpeed = 4;
    private Vector3 moveDir = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        GridGenerator.OngridIsDone += MoveToStartPos;
        MoveToStartPos();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CalculateMovePosition();
    }

    private void MoveToStartPos()
    {
        float gridXpos = gridGenerator.width / 2;
        float gridYpos = gridGenerator.height / 2;

        transform.position = new Vector3(gridXpos, 10, gridYpos);
    }

    private Vector3 CalculateMovePosition()
    {
        GetZoom();
        GetXInput();
        GetYInput();

        return new Vector3(moveDir.x, moveDir.y, moveDir.z);
    }

    private void GetZoom()
    {
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(scrollValue);
        moveDir.y += scrollValue * zoomSpeed * -1;

    }

    private float GetXInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x += 1 * moveSpeed;
            return moveDir.x;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDir.x += -1 * moveSpeed;
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
            moveDir.z += 1 * moveSpeed;
            return moveDir.z;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDir.z += -1 * moveSpeed;
            return moveDir.z;
        }
        else
        {
            return 0;
        }

    }

}
