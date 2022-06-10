using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveInGame : MonoBehaviour
{
    [Header("InUiDetails")]
    [SerializeField]private string nameOfGame;
    [SerializeField]private int stageOfSave;

    [Header("InSceneDetails")] 
    [SerializeField]private Transform _player;

    [Header("InScriptsDetails")]
    IObservable _playerToCopy;
    public PlayerMood playerMood;
    private float _life;
    //public static List<UserDetails.Achievments> _achievments = new List<UserDetails.Achievments>();
    private SaveInGame instance;

    public Event Action;
    //private List<UserDetails> _playersData = new List<UserDetails>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        SaveGame.instance.OnLoadLevelData += LoadData;
        //_playerToCopy = playerMood;
        //_playerToCopy.Subscribe(this);
    }
    public void Save()
    {
        SaveGame.SetNumber(stageOfSave);
        string _scene = SceneManager.GetActiveScene().name;
        //SaveGame.instance.savedGames.life = _life;
        SaveGame.instance.savedGames.name = nameOfGame;
        SaveGame.instance.savedGames.scene = _scene;
        SaveGame.instance.savedGames.playerTransform = _player.transform.position;
        //.instance.savedGames.triggeredEvents = EventManager._triggeredEvents;
        SaveGame.instance.Save();
    }
    // public void Notify(float value, float maxValue)
    // {
    //     _life = value;
    // }
    public void Load()
    {
        SaveGame.SetNumber(stageOfSave);
        SaveGame.instance.CheckLoad();
    }
    
    public void LoadData(SavedGames data)
    {
        PlayerStats.SetPosition(SaveGame.instance.savedGames.playerTransform);
        PlayerStats.SetLoad(true);
        SceneManager.LoadScene(SaveGame.instance.savedGames.scene);
    }
}