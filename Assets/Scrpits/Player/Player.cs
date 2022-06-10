using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _movementSpeedParameterName = "MovementSpeed";
    [SerializeField]
    private string _runningParameterName = "Running";
    [SerializeField]
    private string _jumpingParameterName = "IsJumping";
    [SerializeField]
    private string _groundedParameterName = "IsGrounded";

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private ForceMode jumpForceMode = ForceMode.Force;
    [SerializeField]
    private Rigidbody _rb;

    Control _control;
    Movement _movement;
    [SerializeField]
    private Transform _camTransform;
    
    private Vector3 _velocity;

   
    private void Start()
    {
        
        _camTransform = Camera.main.transform;

        _movement = new Movement(transform, _speed, _jumpForce, _rb, _camTransform, _velocity);
        
        _control = new Control(_movement, _animator, _runningParameterName, _movementSpeedParameterName, _groundedParameterName, _jumpingParameterName);
        
        EventManager.Subscribe("ResetLevel2",ResetLevel2);

    }
    void FixedUpdate()
    {
        _control.OnUpdate();
    }
    public Vector3 GetVelocity()
    {
        return _velocity;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject)
        {
            _control.Jump();
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject.GetComponent<ICollectable>();

        if (Input.GetKeyDown(KeyCode.E) && obj != null) obj.Collect();
    }*/

    void ResetLevel2(params object[] parameters)
    {
        transform.position = ManagerGame._level2SpawnPoint.transform.position;
    }

}
