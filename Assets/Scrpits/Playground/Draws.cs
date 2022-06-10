using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draws : MonoBehaviour, ICollectable
{
    [SerializeField]
    private ParticleSystem _slowParticles;
    public PlaygroundManager _playgroundManager;
    public Flower _flower;
    
    public void Collect()
    {
        AudioManager.instance.Play(AudioManager.Types.CollectedToy);
        //paper sound
        Instantiate(_slowParticles, transform.position, transform.rotation);
        gameObject.SetActive(false);
        _playgroundManager.AddDraws(1);
        _flower.ChangeColor();
    }
}
