using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleGirl : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private Transform player;
    [Header("Little Girl")]
    [SerializeField]
    private GameObject littleGirl;
    [Header("Materials")]
    [SerializeField]
    private Material Material1;
    [SerializeField]
    private Material Material2;
    [Header("Effects")]
    [SerializeField]
    private ParticleSystem _particleSystem;
    private bool _inPlatform = false;

    public void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void Start()
    {
        EventManager.Subscribe("EventLGirl", FirstDot);
        EventManager.Subscribe("EntersRoom2", SecondDot);
        EventManager.Subscribe("EntersPlatform", ThirdDot);
        EventManager.Subscribe("ToyQuest", FourthDot);
        EventManager.Subscribe("Level2Won", FithDot);
    }
    private void Update()
    {
        transform.LookAt(player);
    }

    private void FixedUpdate()
    {
        if (Accessor.isChatting == true && _inPlatform == true)
            MovingDot();
    }
    void  FirstDot(params object[] parameters)
   {
        //Instantiate(littleGirl, DotManager.instance.dots[0].dots[0].transform.position, Quaternion.identity);
        transform.position = DotManager.instance.dots[0].dots[0].transform.position;
        littleGirl.GetComponent<MeshRenderer>().material = Material2;
        Instantiate(_particleSystem, transform.position, transform.rotation);
        EventManager.UnSubscribe("EventLGirl", FirstDot);
   }
    void SecondDot(params object[] parameters)
    {
        transform.position = DotManager.instance.dots[0].dots[1].transform.position;
        littleGirl.GetComponent<MeshRenderer>().material = Material1;
        Instantiate(_particleSystem, transform.position, transform.rotation);
        EventManager.UnSubscribe("EntersRoom2", SecondDot);
    }
    void ThirdDot(params object[] parameters)
    {
        _inPlatform = true;
        littleGirl.GetComponent<MeshRenderer>().material = Material1;
        Instantiate(_particleSystem, transform.position, transform.rotation);
        EventManager.UnSubscribe("EntersPlatform", ThirdDot);
    }
    void FourthDot(params object[] parameters)
    {
        transform.position = DotManager.instance.dots[0].dots[3].transform.position;
        littleGirl.GetComponent<MeshRenderer>().material = Material2;
        Instantiate(_particleSystem, transform.position, transform.rotation);
        EventManager.UnSubscribe("ToyQuest", FourthDot);
    }
    void FithDot(params object[] parameters)
    {
        transform.position = DotManager.instance.dots[0].dots[4].transform.position;
        littleGirl.GetComponent<MeshRenderer>().material = Material2;
        Instantiate(_particleSystem, transform.position, transform.rotation);
        EventManager.UnSubscribe("Level2Won", FithDot);
    }
    void MovingDot()
    {
        transform.position = DotManager.instance.dots[0].dots[2].transform.position;
    }
}
