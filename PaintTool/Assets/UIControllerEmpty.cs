using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using AmazingTool;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using UnityEngine.Assertions;
using System.Xml.Serialization;
using UnityEngine.UIElements;
using SFB;

namespace AmazingTool
{
    [System.Serializable]
    public class TestData
    {
        public string name = "default";
        
        public string lastName = "defaultLastName";
        public float someFloat = 10f;
        public int someInt = 10;
        public List<int> someListOfData = new List<int>();
	}
}

public class UIControllerEmpty : MonoBehaviour
{
    public TextField fileNameInput;
    public Button createButton, saveButton, loadButton;
    public TestData myData;

    // TODO: Move this to a class of some sort, maybe with a generic / interface for applying/updating?
    public TextField nameField, intField, floatField;
    public VisualElement dataEditor;

    // Start is called before the first frame update
    void Start()
    {
		#region UI Init
		var root = GetComponent<UIDocument>().rootVisualElement;

        // file input field
        fileNameInput = root.Q<TextField>("filename");

        // get top level buttons
        createButton = root.Q<Button>("create");
        saveButton = root.Q<Button>("save");
        loadButton = root.Q<Button>("load");

        // get data editor & child name field
        dataEditor = root.Q<IMGUIContainer>("data-editor");
        nameField = dataEditor.Q<TextField>("name");
        intField = dataEditor.Q<TextField>("int");
        floatField = dataEditor.Q<TextField>("float");

        // implement button reactions
        createButton.clicked += CreateButton_clicked;
        saveButton.clicked += SaveButton_clicked;
        loadButton.clicked += LoadButton_clicked;

        StartCoroutine(ChangeChecker());
        #endregion
	}

    IEnumerator ChangeChecker() {
        // TODO: Focus Events...
        while( Application.isPlaying ) {
            ApplyChanges();
            yield return new WaitForSeconds(1f);
		}

        Debug.Log("EXIT");
	}

	private void CreateButton_clicked() {
        Debug.Log("New Data Created!");
	}

    public void LoadButton_clicked() {
		string[] options = StandaloneFileBrowser.OpenFilePanel("Open File", Application.persistentDataPath, "txt", false);
		if (options.Length == 0) return;

		string url = options[0];
        
        Debug.Log("Attempting Load");
	}

    public void SaveButton_clicked() {
        Debug.Log("Attempting Save");
	}

	#region UI stuff

	private void ApplyChanges() {
        if (myData == null) myData = new TestData();

        myData.name = nameField.text;
        myData.someInt = SanitizeInt(intField);
        myData.someFloat = SanitizeFloat(floatField);
    }

    private int SanitizeInt( TextField field ) {
        string sanitized;
        int retVal = 0;
        try
        {
            sanitized = Regex.Replace(field.text, @"[^-+0-9]", "");//"[^0-9]"
            retVal = int.Parse(sanitized);
            sanitized = retVal.ToString();
            field.SetValueWithoutNotify(sanitized);
        }
        catch (System.OverflowException e)
        {
			retVal = int.MaxValue;
			sanitized = retVal.ToString();
			field.SetValueWithoutNotify(sanitized);
		}
        catch (System.FormatException e)
        {
            if (field.panel.focusController.focusedElement == field)
                sanitized = field.text;
            else
            {
                Debug.LogWarning("Format exception: " + e.Message);
                sanitized = "0";
                field.SetValueWithoutNotify(sanitized);
            }
        }
        return retVal;
    }

    private float SanitizeFloat( TextField field ) {
        string sanitized;
        float retVal = 0;
        try {
            sanitized = Regex.Replace(field.text, @"[^-+0-9\.eE]", ""); //"[^-0-9.]"
			retVal = float.Parse(sanitized);
            sanitized = retVal.ToString();
            field.SetValueWithoutNotify(sanitized);
        }
        catch ( System.FormatException e ) {
			if (field.panel.focusController.focusedElement == field)
				sanitized = field.text;
            else
            {
				Debug.LogWarning("Format exception: " + e.Message);
				sanitized = "0";
				field.SetValueWithoutNotify(sanitized);
			}				
        }
        return retVal;
    }

    private void UpdateEditorDisplay() {
        nameField.SetValueWithoutNotify(myData.name);
        intField.SetValueWithoutNotify(myData.someInt.ToString()); 
        floatField.SetValueWithoutNotify(myData.someFloat.ToString());
    }
    #endregion
}