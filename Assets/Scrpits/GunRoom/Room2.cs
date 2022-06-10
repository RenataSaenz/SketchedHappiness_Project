using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room2 : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    private GameObject _wonLevelPanel = null;
    [Header("Text")]
    [SerializeField]
    private Text _wonLevelText;
    [Header("Strings")]
    [SerializeField]
    private string LevelInsctructions = "You errased them all.";

    [SerializeField]
    private GameObject _memorie1;
    [SerializeField]
    private GameObject _memorie2;
    private bool _wonSet;

    void Start()
    {
        _wonSet = false;
        _memorie1.SetActive(false);
        _memorie2.SetActive(false);
        _wonLevelPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventManager.Trigger("EntersRoom2");
            _memorie1.SetActive(true);
            _memorie2.SetActive(true);
        }
    }
    public void YouWon()
    {
        _wonLevelPanel.SetActive(true);
        _wonLevelText.text = LevelInsctructions;
    }
    private void OnTriggerStay(Collider other)
    {
        if(_wonSet == true)
        {
            Invoke("YouWon", 0f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _wonLevelPanel.SetActive(false);
    }
}
