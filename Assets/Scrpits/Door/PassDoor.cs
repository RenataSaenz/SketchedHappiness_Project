using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassDoor : MonoBehaviour
{
    [SerializeField]
    private Collider _colliderAddLife;
    [SerializeField]
    int _addMood = 0;
    [SerializeField]
    private GameObject _door;
    Door _doorScript;
    [SerializeField]
    private GameObject _entersPass;
    EntersPassCollider _entersPassColliderScript;

    private void Start()
    {
        _entersPassColliderScript = _entersPass.GetComponent<EntersPassCollider>();
        _doorScript = _door.GetComponent<Door>();
        _colliderAddLife.enabled = false;
    }
    private void Update()
    {
        ActivateCollider();
    }
    void ActivateCollider()
    {
        if (_doorScript.isOpen == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Collider activated");
            _colliderAddLife.enabled = true;
        }

        if (_doorScript.isOpen == false)
        {
            _colliderAddLife.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _entersPassColliderScript.entersDoor == true)
        {
            //_managerMood.AddPoints(_addMood);
            var damageable = other.transform.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.SubtractLifeFunc(_addMood);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          //  Debug.Log("Collider desactivated");
            _colliderAddLife.enabled = false;
            _entersPassColliderScript.entersDoor = false;
        }
            
    }

}
