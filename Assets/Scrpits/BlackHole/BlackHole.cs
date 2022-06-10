using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField]
    private GameObject thisBlackHole;
    [SerializeField]
    private Transform toBlackHole;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform soul;

    private void Start()
    {
        //toBlackHole.position = toBlackHole.transform.position;
    }
    
    void OnTriggerEnter(Collider trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            player.transform.position = toBlackHole.transform.position;
            soul.transform.position = toBlackHole.transform.position;
            Debug.Log("Black Hole");
        }
        if(trig.gameObject.tag == "Platform")
        {
            trig.gameObject.SetActive(false);
        }
    
    }
}
