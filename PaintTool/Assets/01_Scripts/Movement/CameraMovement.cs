using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //[SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float zoomSpeed = 4;
    [SerializeField] private Vector3 beginPosition;
    private Vector3 moveDir;

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
        //float gridYpos = gridGenerator.height / 2;
       // float gridXpos = gridGenerator.width / 2;

        moveDir = beginPosition;
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
        moveDir.y += (scrollValue * zoomSpeed) * Time.deltaTime * -1;

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

}
