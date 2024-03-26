using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class SaveCheck : MonoBehaviour
{
    public static Action OnNewProjectButtonClicked;
    public static Action OnLoadButtonClicked;
    public static Action OnExportButtonClicked;



    [SerializeField] private TMP_Text SaveButtonText;
    [SerializeField] private GameObject UIPopUpPanel;
    [SerializeField] private GameObject newProjectUIPopUp;
    [SerializeField] private GameObject exportUIPopUp;
    [SerializeField] private GameObject loadUIPopUp;
    [SerializeField] private GameObject currentPopUp;


    [Header("UI Buttons")]
    [SerializeField] private Button saveButton;
    [SerializeField] private Button cancelButton;


    bool FileIsSaved = false;

    private void Start()
    {

        DrawingTool.OnInput += SetFileIsSavedFalse;
        Saving.OnSaving += HasSaved;
        SaveButtonText.text = "Save*";
    }


    public bool CheckIfSaved()
    {
        if (FileIsSaved == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetFileIsSavedFalse()
    {
        Debug.Log("there is input");
        FileIsSaved = false;
        SaveButtonText.text = "Save*";
    }

    public void HasSaved()
    {
        CancelButtonClicked();
        SaveButtonText.text = "Save";
        FileIsSaved = true;
    }

    public void LoadPanel()
    {
        currentPopUp = loadUIPopUp;
        if (CheckIfSaved() == true)
        {
            LoadButtonClicked();
        }
        else
        {
            UIPopUpPanel.SetActive(true);
            currentPopUp.SetActive(true);
        }
    }
    public void NewProjectPanel()
    {
        currentPopUp = newProjectUIPopUp;
        if (CheckIfSaved() == true)
        {
            NewProjectButtonClicked();
        }
        else
        {
            UIPopUpPanel.SetActive(true);
            currentPopUp.SetActive(true);
        }
    }
    public void ExportPanel()
    {
        currentPopUp = exportUIPopUp;
        if (CheckIfSaved() == true)
        {
            ExportButtonClicked();
        }
        else
        {
            Debug.Log(" export");
            UIPopUpPanel.SetActive(true);
            currentPopUp.SetActive(true);
        }
    }

    public void CancelButtonClicked()
    {
        UIPopUpPanel.SetActive(false);
        currentPopUp.SetActive(false);
    }

    public void NewProjectButtonClicked()
    {
        Debug.Log("activate new project");
        OnNewProjectButtonClicked?.Invoke();
    }
    public void LoadButtonClicked()
    {
        Debug.Log("activate  load");

        OnLoadButtonClicked?.Invoke();
    }
    public void ExportButtonClicked()
    {
        Debug.Log("activate export");

        OnExportButtonClicked?.Invoke();
    }


}
