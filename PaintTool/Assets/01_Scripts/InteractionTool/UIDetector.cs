using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class UIDetector : MonoBehaviour
{
    public static event Action<bool> OnUIHover;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                ToolColor toolColor = result.gameObject.GetComponent<ToolColor>();
                if (toolColor != null)
                {
                    DrawingTool.OnColorChange?.Invoke(toolColor.color);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count == 0)
        {
            OnUIHover?.Invoke(true);
        }
        else
        {
            OnUIHover?.Invoke(false);
        }
    }
}