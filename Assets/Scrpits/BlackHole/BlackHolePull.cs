using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]

public class BlackHolePull : MonoBehaviour
{
    [SerializeField]
    private float _gravityPull = .78f;
    [SerializeField]
    private float _gravityRadius = 1f;
    void Awake()
    {
        _gravityRadius = GetComponent<SphereCollider>().radius;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody)
        {
            float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / _gravityRadius;
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * _gravityPull * Time.smoothDeltaTime);
            Debug.DrawRay(other.transform.position, transform.position - other.transform.position);
        }
    }
}

