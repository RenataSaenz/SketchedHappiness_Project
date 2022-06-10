using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaygroundManager : MonoBehaviour
{
    [Header("Counters")] [SerializeField] private int _totalDraws;
    [SerializeField] private int _draws;

    [SerializeField] private Text _drawsCounterText;
    
    public Dialogue[] dialogue;
    public float _timeBeforeGoingHome = 3f;
    public static PlaygroundManager instance;
    [SerializeField] private GameObject _counterCanvas;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        _counterCanvas.SetActive(false);
        AudioManager.instance.Play(AudioManager.Types.ScaryViolins);
        DialogueManager.instance.StartDialogue(dialogue[0]);
    }

    public void AddDraws(int value)
    {
        _draws += value;
        _drawsCounterText.text = "Total Draws: " + _draws.ToString() + " / " + _totalDraws.ToString();
        DialogueManager.instance.StartDialogue(dialogue[_draws]);
        EndLevel();
    }

    public void EndLevelDoor()
    {
        AudioManager.instance.Play(AudioManager.Types.FairyWand);
        DialogueManager.instance.StartDialogue(dialogue[4]);
        _counterCanvas.SetActive(true);
    }
    void EndLevel()
    {
        if (_draws == _totalDraws)
        {
            StartCoroutine(GoHome());
        }
    }
    
    public IEnumerator GoHome()
    {
        yield return new WaitForSeconds(_timeBeforeGoingHome);
        SceneManager.LoadScene("Level1");
    }
}
