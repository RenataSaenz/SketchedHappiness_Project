using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGame : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private int _finalEnd = -30;
    [SerializeField]
    private float _enemy;

    [Header("SceneTransitions")]
    //public GameObject pauseMenu;
    [SerializeField]
    private GameObject _gameOver;
    [SerializeField]
    private GameObject _youWin;
    [SerializeField]
    private GameObject _inventory;
    [SerializeField]
    private GameObject _backToMenu;

    [Header("Levels")]
    [SerializeField]
    private int _levelsWon;
    public static GameObject _level2SpawnPoint;
    private bool _levelReset = false;
    [SerializeField]
    private float _timeBeforeRespawn = 1.5f;
    

    // [Header("LoadStats")] 
    // private float _life;
    // private Vector3 _position;
    //private List<UserDetails> _playersData = new List<UserDetails>();

    public static ManagerGame instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        
        EventManager.Clear();
        Cursor.visible = false;

        _inventory.SetActive(false);
        _gameOver.SetActive(false);
        _youWin.SetActive(false);
        _backToMenu.SetActive(false);
    }
    private void Start()
    {
        _levelsWon = 0;

        AudioManager.instance.Play(AudioManager.Types.HorrorAmbience);

        TimeContinues();
        EventManager.Subscribe("StartsLevel2", ResetLevel2);
        EventManager.Subscribe("AfterFirstEnemySawn", WonLevel1);
        EventManager.Subscribe("Level2Won", WonLevel2);
        EventManager.Subscribe("GameOver", GameOverPanel);

        bool _loaded = PlayerStats.GetLoad();

        if (_loaded == true)
        {
            float life = PlayerStats.GetLife();
            Vector3 position = PlayerStats.GetPosition();
            
            _player.transform.position = position;
            var damageable = _player.GetComponent<IDamageable>();
            damageable.StartLifeFunc(life);

            PlayerStats.SetLoad(false);

        }
    }

    public void Update()
    {
        if (_player.transform.position.y <= _finalEnd)
            EventManager.Trigger("GameOver");

        if (Input.GetKeyDown(KeyCode.Escape))
            Menu();
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            TimeContinues();
            _backToMenu.SetActive(false);
        }
        if ((Input.GetKey(KeyCode.Tab)))
            _inventory.SetActive(true);
        else
            _inventory.SetActive(false);

        if (_levelsWon > 1)
        {
            YouWin();
        }  
    }
    public void ResetLevel2(params object[] parameters)
    {
        _levelReset = true;
    }
    
    public void WonLevel1(params object[] parameters)
    {
        _levelReset = false;
        _levelsWon ++;
        EventManager.UnSubscribe("AfterFirstEnemySawn", WonLevel1);
    }
    public void WonLevel2(params object[] parameters)
    {
        _levelsWon++;
        _levelReset = false;
        EventManager.UnSubscribe("ResetLevel2", WonLevel2);
        EventManager.UnSubscribe("Level2Won", WonLevel2);
    }

    private void GameOverPanel(params object[] parameters)
    {
        TimeStops();
        _gameOver.SetActive(true);
    }
    private void YouWin()
    {
        TimeStops();
        _youWin.SetActive(true);
    }
    private void Menu()
    {
        TimeStops();
        _backToMenu.SetActive(true);
    }
    private void TimeContinues()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    private void TimeStops()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

}
