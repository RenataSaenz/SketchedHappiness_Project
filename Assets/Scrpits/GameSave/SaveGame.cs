using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour
{
    public static SaveGame instance;

     static int _numberGame;
    public SavedGames savedGames;
    //string _fileName = "Game{0}Data.sav";
    private string _fileName = "Saved{0}Games";
    string _saveFilePath;

    [SerializeField] private GameObject _warningCanvas;
    [SerializeField] private Text _warningText;
    
    public event Action<SavedGames> OnLoadLevelData;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //_saveFilePath = Application.persistentDataPath + "/" + string.Format(_fileName, _numberGame);
        
    }
    
    public static void SetNumber(int n)
    {
        _numberGame = n;
    }


    private void Start()
    {
        _warningCanvas.SetActive(false);
        //Load();
    }

    public void Save()
    {
        _saveFilePath = Application.persistentDataPath + "/" + string.Format(_fileName, _numberGame);
        Debug.Log(_saveFilePath);
        try
        {
            StreamWriter streamWriter = new StreamWriter(_saveFilePath, false);
            streamWriter.Write(savedGames.ToJson());
            streamWriter.Close();
        }
        catch (Exception e)
        {
            Debug.LogError(e); 
        }
    }

    public void CheckLoad()
    {
        _saveFilePath = Application.persistentDataPath + "/" + string.Format(_fileName, _numberGame);
        
        if (System.IO.File.Exists(_saveFilePath))
        {
            Load();
        }
        else
        {
            _warningCanvas.SetActive(true);
            _warningText.text = "Empty Slot";
            Debug.Log(_saveFilePath);
        }
    }

    public void Load()
    {
        try
        {
            if (savedGames == null)
                savedGames = new SavedGames();

            if (File.Exists(_saveFilePath))
            {
                StreamReader streamReader = new StreamReader(_saveFilePath);
                savedGames = JsonUtility.FromJson<SavedGames>(streamReader.ReadToEnd());
                streamReader.Close();
                
                OnLoadLevelData?.Invoke(savedGames);
            }
            else
            {
                File.Create(_saveFilePath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        
    }
    
}
