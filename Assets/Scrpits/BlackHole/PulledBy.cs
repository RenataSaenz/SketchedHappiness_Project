using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulledBy : MonoBehaviour
{
    [SerializeField]
    private Transform blackHole;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject platforms;
    private void Start()
    {
        if (blackHole == null)
            blackHole = GameObject.FindWithTag("SphereBlackHole").transform;
    }

    private void Update()
    {
        transform.LookAt(blackHole);
        transform.position += transform.forward * _speed * Time.deltaTime;

        if (transform.position == blackHole.position)
        {
            platforms.SetActive(false);
        }
    }
}
