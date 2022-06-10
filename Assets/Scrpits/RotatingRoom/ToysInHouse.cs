using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToysInHouse : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectsWon;
    [SerializeField]
    private GameObject _particlesGrey;
    [SerializeField]
    private GameObject _lightColor;
    [SerializeField]
    private bool _lifeSpot = false;
    
    void Start()
    {
        EventManager.Subscribe("Level2Won", QuestWon);
        _objectsWon.SetActive(false);
        _particlesGrey.SetActive(true);
        _lightColor.SetActive(false);
    }
    private void OnTriggerEnter(Collider with)
    {
        if (with.gameObject.layer == Layers.PLAYER)
        {
            EventManager.Trigger("ToyQuest");
        }
    }
    private void QuestWon(params object[] parameters)
    {
        _lifeSpot = true;
        _objectsWon.SetActive(true);
        _particlesGrey.SetActive(false);
        _lightColor.SetActive(true);
    }
    private void OnTriggerStay(Collider with)
    {
        if (_lifeSpot == true)
        {
            var damageable = with.transform.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.AddLifeFunc(1);
            }
        }
        
    }
}
