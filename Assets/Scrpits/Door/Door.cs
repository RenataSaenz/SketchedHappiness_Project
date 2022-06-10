using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, ICollectable
{
    [SerializeField]
    private AudioSource _sound;

    [SerializeField]
    private GameObject OpenPanel = null;
    [SerializeField]
    private Text panelText;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string OpenText = "Press 'E' to open";
    [SerializeField]
    private string CloseText = "Press 'E' to close";
    [Header("Bloqued Door")]
    [SerializeField]
    private string BloquedText = "Find the key to the door";
    [SerializeField]
    private bool _bloqued;

    private int _timeBeforeClosing = 12;

    public bool isOpen = false;
    private bool _isInsideTrigger = false;

    private void Start()
    {
        //EventManager.Subscribe("KeyEDown", ActionDoor);
        OpenPanel.SetActive(false);
    }

    public void Collect()
    {
        ActionDoor();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Int_Area")
        {
            _isInsideTrigger = true;
            OpenPanel.SetActive(true);
            UpdatePanelText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Int_Area")
        {
            _isInsideTrigger = false;
            //animator.SetBool("Open", false);
            OpenPanel.SetActive(false);

        }
    }

    public bool IsOpenPanelActive
    {
        get
        {
            return OpenPanel.activeInHierarchy;
        }
    }

    public void ActionDoor()
    {
        if(IsOpenPanelActive && _isInsideTrigger)
        {
            if(_bloqued == false)
            {
                isOpen = !isOpen;
                if (isOpen == true)
                {
                    animator.SetBool("Open", true);
                    StartCoroutine(CloseDoor());

                }
                if (isOpen == false)
                    animator.SetBool("Open", false);
                //AudioManager.instance.Play(AudioManager.Types.Door);
                _sound.Play(0);
            }
            Invoke("UpdatePanelText", 1.0f);
        }
    }

    public virtual void UpdatePanelText()
    {
        if(panelText != null && _bloqued == false)
        {
            panelText.text = isOpen ? CloseText : OpenText;
        }
        else if (panelText != null && _bloqued == true)
            panelText.text = BloquedText;
    }

    public IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(_timeBeforeClosing);
        isOpen = false;
        animator.SetBool("Open", false);
        //AudioManager.instance.Play(AudioManager.Types.Door);
        _sound.Play(0);
    }

}
