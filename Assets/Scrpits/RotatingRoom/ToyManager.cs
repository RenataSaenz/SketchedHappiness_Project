using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyManager : MonoBehaviour
{
    //ManagerMood _managerMood;

    [Header("Area")]
    [SerializeField]
    private float _endsArea = -30;
    [SerializeField]
    private GameObject _player;

    [Header("Panels")]
    [SerializeField]
    private GameObject _allToysCollectedPanel = null;
    [SerializeField]
    private GameObject OpenPanel = null;
    [SerializeField]
    private GameObject _totalToysTextPanel = null;
    [SerializeField]
    private GameObject _timeTextPanel = null;
    [SerializeField] private GameObject _detentionPanel = null;

    [Header("Text")]
    [SerializeField]
    private Text _allCollectedText;
    [SerializeField]
    private Text _panelText;
    [SerializeField]
    private Text _totalToysText;
    [SerializeField]
    private Text _timeText;
    [SerializeField] private Text _totalDetention;

    [Header("Counters")]
    [SerializeField]
    private int _totalToys;
    [SerializeField]
    private int _collectedToys;
    [SerializeField]
    private float timeValue = 90;
    private float _startingTimeValue;


    [Header("Strings")]
    [SerializeField]
    private string LevelInsctructions = "Find all the toys.";
    [SerializeField]
    private string AllCollected = "You have all the toys! Return to the front door.";
    [SerializeField]
    private string YouWon = "You collected all the toys in time, congratulations!";

    private bool _startsLevelWarn;
    private bool _triggerWon = false;

    [SerializeField]
    private float _timeBeforeRespawn = 1.5f;

    [SerializeField] private List<Toy> _toysInGame;

    private void Start()
    {
       // _managerMood = GameObject.Find("ManagerMood").GetComponent<ManagerMood>();

        _startingTimeValue = timeValue;

        _startsLevelWarn = false;
        OpenPanel.SetActive(false);
        _totalToysTextPanel.SetActive(false);
        _timeTextPanel.SetActive(false);
        _allToysCollectedPanel.SetActive(false);
        _detentionPanel.SetActive(false);
    }
    public void AddToys(int value)
    {
        _collectedToys += value;
       
        if (_collectedToys == _totalToys)
        {
            Invoke("AllToysCollected", 0f);
        }
    }
    public void AllToysCollected()
    {
        _allToysCollectedPanel.SetActive(true);
        _allCollectedText.text = AllCollected;
    }
    public void InstructionsText()
    {
        OpenPanel.SetActive(true);
        _panelText.text = LevelInsctructions;
    }
    public void YouWonText()
    {
        OpenPanel.SetActive(true);
        _panelText.text = YouWon;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventManager.Trigger("StartsLevel2");

            _allToysCollectedPanel.SetActive(false);
            StartsLevel();
            if(_collectedToys != _totalToys)
            {
                Invoke("InstructionsText", 0f);
            }
            if (timeValue > 0 && _collectedToys == _totalToys)
            {
                _startsLevelWarn = false;
                Invoke("YouWonText", 0f);
                _startsLevelWarn = false;
                _totalToysTextPanel.SetActive(false);
                _timeTextPanel.SetActive(false);
                _detentionPanel.SetActive(false);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OpenPanel.SetActive(false);
        }
    }

    private void StartsLevel()
    {
        _startsLevelWarn = true;
    }

    private void Update()
    {
        if (_player.transform.position.x > _endsArea)
        {
            OpenPanel.SetActive(false);
            _startsLevelWarn = false;
            _totalToysTextPanel.SetActive(false);
            _timeTextPanel.SetActive(false);
            _detentionPanel.SetActive(false);
            
            timeValue = _startingTimeValue;

            if (_collectedToys == _totalToys)
            {
                EventManager.Trigger("Level2Won");
            }
            else if (_collectedToys < _totalToys)
            {
                _collectedToys = 0;
                foreach (var toy in _toysInGame)
                {
                    toy.ResetToys();
                }
            }
                
        }
        if (_startsLevelWarn == true)
        {
            _totalToysTextPanel.SetActive(true);
            _timeTextPanel.SetActive(true);
            _detentionPanel.SetActive(true);
            
            _totalDetention.text = "Times in detention: " + Accessor._counter.ToString();
            _totalToysText.text = _collectedToys.ToString() + " / " + _totalToys.ToString();

            if (timeValue > 0)
                timeValue -= Time.deltaTime;
            else if (timeValue <= 0)
            {
                //you lose
                timeValue = 0;
                StartCoroutine(RespawnAfterTime());
                
            }
            DisplayTime(timeValue);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        
    }
    
    private IEnumerator RespawnAfterTime()
    {
        yield return new WaitForSeconds(_timeBeforeRespawn);
        EventManager.Trigger("ResetLevel2");
    }
}



