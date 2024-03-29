using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera PlayerCam;
    //public ArduinoHandler ArduinoHandler;

    [SerializeField] private float minFOV = 15f;
    [SerializeField] private float maxFOV = 60;
    private float richtingscoefficient;
    private float FOVchange;
    private float velocity = 0f;
    private float smoothTime = 0.25f;

    private float scrollValue;
    private float zoomValue = 100;
    // Start is called before the first frame update
    void Start()
    {
        richtingscoefficient = (maxFOV / minFOV) / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //scrollValue = Input.GetAxis("Mouse ScrollWheel");
        PlayerCam.orthographicSize = GetZoom();
        //PlayerCam.orthographicSize = Mathf.SmoothDamp(PlayerCam.fieldOfView, GetZoom(), ref velocity, smoothTime);

    }
    private float CalculationZoom2()
    {
        float currentFOV;
        FOVchange = richtingscoefficient * scrollValue;
        if (FOVchange <= 1)
        {
            FOVchange = 1;
        }

        currentFOV = minFOV * FOVchange;

        //Debug.Log("potValue : " + potValue + "richtingscoefficient : " + richtingscoefficient + " FOVchange : " + FOVchange + " currentFOV : " + currentFOV);
        return currentFOV;
    }
    private float GetZoom()
    {
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        zoomValue += (scrollValue * 1000) * Time.deltaTime * -1;
        Debug.Log(zoomValue);
        return zoomValue;

    }


}
