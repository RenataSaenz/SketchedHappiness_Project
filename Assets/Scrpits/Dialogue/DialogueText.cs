using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    public Dialogue[] dialogue;
    private bool _toyQuestWon = false;

    public void Start()
    {
        EventManager.Subscribe("EventLGirl", FirstDot);
        EventManager.Subscribe("EntersPlatform", ThirdDot);
        EventManager.Subscribe("ToyQuest", FourthDot);
        EventManager.Subscribe("Level2Won", FithDot);
        EventManager.Subscribe("EntersRoom2", SecondDot);


    }
    void FirstDot(params object[] parameters)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[0]);
        EventManager.UnSubscribe("EventLGirl", FirstDot);
    }
    void ThirdDot(params object[] parameters)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[1]);
        EventManager.UnSubscribe("EntersPlatform", ThirdDot);
    }

    void FourthDot(params object[] parameters)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[2]);
        EventManager.UnSubscribe("ToyQuest", FourthDot);
    }
    void FithDot(params object[] parameters)
    {
        _toyQuestWon = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[3]);
        EventManager.UnSubscribe("Level2Won", FithDot);
    }
    void SecondDot(params object[] parameters)
    {
        if (_toyQuestWon)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue[4]);
            EventManager.UnSubscribe("EntersRoom2", SecondDot);
        }
        else if (!_toyQuestWon)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue[5]);
        }
        
    }

}
