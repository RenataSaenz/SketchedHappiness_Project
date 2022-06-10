using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private ParticleSystem pollen;
    [SerializeField] private GameObject pollenSpawner;
    [SerializeField]private float _pollenTimer;
    private float _restartPollenTimer;
    [SerializeField]private Sprite _BWSprite;
    [SerializeField]private GameObject _flower;
    void Start()
    {
        _restartPollenTimer = _pollenTimer;
    }

    private void FixedUpdate()
    {
        if (pollenSpawner.transform.childCount <= 0)
        {
            _pollenTimer -= Time.deltaTime;
            
            if ( _pollenTimer < 0)
            {
                Instantiate(pollen, pollenSpawner.transform.position, pollenSpawner.transform.rotation,pollenSpawner.transform);
                _pollenTimer = _restartPollenTimer;
            }
        }
    }

    public void ChangeColor()
    {
        _flower.GetComponent<SpriteRenderer>().sprite = _BWSprite;
    }
}
