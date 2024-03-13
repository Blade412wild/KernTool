using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;

public class Saving : MonoBehaviour
{

    [SerializeField] private string fileName;
    [SerializeField] private string name;
    [SerializeField] private int leeftijd;
    [SerializeField] private float height;

    private string fullPath;

    public void OnLoadButton()
    {
        Debug.Log("Attempting Loading");
        string[] options = StandaloneFileBrowser.OpenFilePanel("Open File", Application.persistentDataPath, "txt", false);
        if (options.Length == 0) return;

        string url = options[0];
    }

    public void OnSaveButton()
    {
        Debug.Log("Attempting Saving");
        string filePath = StandaloneFileBrowser.SaveFilePanel("Save File", Application.persistentDataPath, "Wow_Je_Saved_Een_Keer.txt", "txt");
        if (filePath.Length == 0) return;
        Save(filePath);
    }

    private void Save(string _filePath)
    {
        StreamWriter writer = new StreamWriter(_filePath);
        writer.WriteLine(JsonUtility.ToJson(this, true));
        writer.Close();
        writer.Dispose();
        Debug.Log("Saved File To : " + _filePath);
    }

    private void Load()
    {

    }

}
