using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public Text nameText;
    public Text dialogueText;

    public Animator animator;


    public Queue<string> sentences_1;
    
    public static DialogueManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        
        sentences_1 = new Queue<string>();
    }
    public void StartDialogue (Dialogue dialogue)
    {
        Accessor.StartChatting(true);
        animator.SetBool("IsOpen", true);

       // Debug.Log("Starting conversation with " + dialogue.name);
        sentences_1.Clear();

        //nameText.text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
        {
            sentences_1.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences_1.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences_1.Dequeue();

        StopAllCoroutines();
        dialogueText.text = sentence;

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Accessor.StartChatting(false);
        animator.SetBool("IsOpen", false);
    }

}