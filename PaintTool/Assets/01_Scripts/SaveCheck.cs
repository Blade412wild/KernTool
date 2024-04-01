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
    [SerializeField] private GameObject QuitUIPopUp;
    [SerializeField] private GameObject loadUIPopUp;
    private GameObject currentPopUp;


    bool FileIsSaved = false;

    private void Start()
    {

        DrawingTool.OnInput += SetFileIsSavedFalse;
        Saving.OnActionDone += HasSaved;
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
            UIPopUpPanel.SetActive(true);
            currentPopUp.SetActive(true);
        }
    }
    public void QuitPanel()
    {
        currentPopUp = QuitUIPopUp;
        if (CheckIfSaved() == true)
        {
            QuitButtonClicked();
        }
        else
        {
            UIPopUpPanel.SetActive(true);
            currentPopUp.SetActive(true);
        }
    }

    public void CancelButtonClicked()
    {
        if (currentPopUp == null) return;
        currentPopUp.SetActive(false);
        UIPopUpPanel.SetActive(false);
    }

    public void NewProjectButtonClicked()
    {

        OnNewProjectButtonClicked?.Invoke();
    }
    public void LoadButtonClicked()
    {
        OnLoadButtonClicked?.Invoke();
    }
    public void ExportButtonClicked()
    {
        OnExportButtonClicked?.Invoke();
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
