using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectGun : MonoBehaviour
{  
    [SerializeField]
    private GameObject _gun;
    private bool _questNotWon;
    private void Start()
    {
        _questNotWon = true;
        EventManager.Subscribe("Level2Won", QuestToysWon);
    }

    void QuestToysWon(params object[] parameters)
    {
        _questNotWon = false;
    }
    
    public void OnTriggerStay(Collider trig)
    {
        if (_questNotWon == false)
        {
            if (trig.gameObject.tag == "Int_Area" && Input.GetKeyDown(KeyCode.E))
            {
                _gun.SetActive(false);
                SceneManager.LoadScene("Playground");
            }
        }
        else if (_questNotWon == true)
        {
            Debug.Log("Help the Ghost Girl find her toys before doing this quest");
        }
    }
}
