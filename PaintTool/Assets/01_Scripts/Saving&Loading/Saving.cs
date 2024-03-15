using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEditor.Progress;

public class Saving : MonoBehaviour
{

    [SerializeField] private string name;
    [SerializeField] private int age;
    [SerializeField] private float height;

    [SerializeField] private Texture2D tex;
    [SerializeField] private CanvasTexture canvasTexture;
    private byte[] encodedPNG;

    private data dataTest;

    private string fullPath;

    public struct data
    {
        public string _name;
        public int _age;
        public float _height;
        public byte[] _encodedPNG;
    }

    private void Start()
    {
        dataTest = new data
        {
            _name = name,
            _age = age,
            _height = height


        };
    }

    public void OnLoadButton()
    {
        Debug.Log("Attempting Loading");
        string[] options = StandaloneFileBrowser.OpenFilePanel("Open File", Application.persistentDataPath, "txt", false);
        if (options.Length == 0) return;
        string filepath = options[0];
        Load(filepath);
    }

    public void OnSaveButton()
    {
        Debug.Log("Attempting Saving");
        string filePath = StandaloneFileBrowser.SaveFilePanel("Save File", Application.persistentDataPath, "Wow_Je_Saved_Een_Keer.txt", "txt");
        if (filePath.Length == 0) return;
        Save(filePath, dataTest);
    }

    private void Save(string _filePath, data dataPackage)
    {
        StreamWriter writer = new StreamWriter(_filePath);
        writer.WriteLine(JsonUtility.ToJson(dataPackage, true));
        writer.Close();
        writer.Dispose();
        Debug.Log("Saved File To : " + _filePath);
    }

    private void Load(string _filePath)
    {
        StreamReader reader = new StreamReader(_filePath);
        data dataSetting = JsonUtility.FromJson<data>(reader.ReadToEnd());
        name = dataSetting._name;
        age = dataSetting._age;
        height = dataSetting._height;
        ImageConversion.LoadImage(tex, dataSetting._encodedPNG);
        encodedPNG = dataSetting._encodedPNG;
        Debug.Log("Saved File To : " + _filePath);
    }

    public void EncodeTexToPNG()
    {
        tex = canvasTexture.texture;
        Debug.Log(tex.name);
        // Encode texture into PNG
        byte[] bytes = ImageConversion.EncodeToPNG(tex);
        dataTest._encodedPNG = bytes;
        Debug.Log("encoded To PNG");
    }

}
