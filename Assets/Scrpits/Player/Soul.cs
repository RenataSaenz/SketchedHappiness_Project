using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soul : MonoBehaviour
{
    #region Variables
    [Header("Soul")]
    [SerializeField]
    private float speed;
    public bool adopted;
    [SerializeField]
    private GameObject power;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float zDistance;
    #endregion

    public void Start()
    {
        zDistance = 7;
        adopted = true;
    }
    public void OnTriggerEnter(Collider trig)
    {
        if (trig.tag == "Player")
        {
            adopted = true;
            Debug.Log("si");
        }
    }
    public void FixedUpdate()
    {
        soulSystem();
    }
    public void soulSystem()
    {
        if (adopted == true)
        {
            Move();
        }
    }
    void Move()
    {
       Vector3 player = transform.position;
        player = this.player.transform.position;

        if (Input.GetMouseButton(1))
        {
            MoveClick();
        }
        else
        {
           gameObject.transform.position = Vector3.MoveTowards(transform.position, player + new Vector3(0f, 0.5f, 0f), speed * Time.deltaTime);
        }
    }
    void MoveClick()
    {
        var mousePos = Input.mousePosition;
        zDistance += Input.GetAxis("Mouse ScrollWheel");

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance));
        Vector3 direction = worldMousePosition;
        
       gameObject.transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);

    }
}
