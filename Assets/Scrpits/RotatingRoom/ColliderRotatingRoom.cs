using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRotatingRoom : RoomManager
{
    [SerializeField]
    private Transform _collider;

    [SerializeField] private GameObject _bridge;

    private bool _rotate = false;

    void Start()
    {
        _bridge.SetActive(false);
    }
    public void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.layer == Layers.PLAYER) 
        {
            //_room.transform.LookAt(_collider);
            _rotate = true;
            AudioManager.instance.Play(AudioManager.Types.Eartquake);
            _bridge.SetActive(true);
            _bridge.transform.localPosition = new Vector3(_collider.localPosition.x, -6.78f, _collider.localPosition.z);
            _bridge.transform.localRotation = _collider.transform.localRotation;
        }
    }
    private void FixedUpdate()
    {
        if (_rotate)
        {
            float angle = Mathf.MoveTowardsAngle(_room.transform.localEulerAngles.y,  _collider.transform.localEulerAngles.y, 15 * Time.deltaTime);
            _room.gameObject.transform.localEulerAngles = new Vector3(0, angle, 0);
            Debug.Log("enters col rot");
            if (_room.transform.localEulerAngles.y == _collider.transform.localEulerAngles.y) _rotate = false;
        }
    }
}
