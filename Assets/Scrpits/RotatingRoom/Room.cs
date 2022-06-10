using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : RoomManager
{
    public float eulerAngY;
    private float _startAngle;
    private float _totalRotation;
    [SerializeField]
    private float _timeBeforeRotating = 2;
    private float _resetTimeBeforeRotating;
    private GameObject _player;
    public int counterToRotateOnce;
    private event Action _rotateRoom;
    int calc = 1;


    public override void Start()
    {
        _resetTimeBeforeRotating = _timeBeforeRotating;
        base.Start();
        counterToRotateOnce = 0;
      
        _player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Feet" && counterToRotateOnce >= 1)
        {
            _player.transform.parent = gameObject.transform;
            eulerAngY = transform.localEulerAngles.y;
            WrapAngle(eulerAngY);
            Accessor.RoomRotatorCounter(1);
            _rotateRoom += RotationEffect;
        }
    }
    
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Feet" && counterToRotateOnce >= 1)
        {
            _timeBeforeRotating-= Time.deltaTime;
            if (_timeBeforeRotating <= 0f)
                Rotate();

            // if (_timeBeforeRotating > 0f)
            //     StartCoroutine(RotateAfterTime());
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Feet")
        {
            calc = 1;
            _player.transform.parent = null;
            _timeBeforeRotating = _resetTimeBeforeRotating;
            //StartCoroutine(ForceResetTime());
            counterToRotateOnce = 0;
        }
    }
    private float WrapAngle(float angle)
    {
        angle %= 360;
        
        if (angle > 180)
            return angle - 360;
        Debug.Log("calculated angle : " + angle.ToString());
         _startAngle = angle;
        return angle;
    }
    private float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;
        //_quaternion = 360 - angle;
        return 360 - angle;
       
    }
    void Rotate()
    {
        counter = Accessor._counter;
        degrees = counter * 90;
        _totalRotation = _startAngle + degrees;
        Debug.Log("room startAngle: " + _startAngle);
        Debug.Log("room script counter: " + counter);
        Debug.Log("Room rotation: " + _startAngle + " + " + degrees + " = " + _totalRotation);

        //gameObject.transform.localEulerAngles = new Vector3(0, _totalRotation, 0);
        
        float angle = Mathf.MoveTowardsAngle(transform.localEulerAngles.y,  degrees, 15 * Time.deltaTime);
        gameObject.transform.localEulerAngles = new Vector3(0, angle, 0);
        _rotateRoom?.Invoke();
        _rotateRoom = delegate { };
    }

    void RotationEffect()
    {
        AudioManager.instance.Play(AudioManager.Types.Eartquake);
        CameraShake.Shake(1f, 0.1f);
    }
    
    private IEnumerator RotateAfterTime()
    {
        yield return new WaitForSeconds(_timeBeforeRotating);
        _rotateRoom?.Invoke();
    }
    private IEnumerator ForceResetTime()
    {
        yield return new WaitForSeconds(_timeBeforeRotating);
    }
}
