using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMood : MonoBehaviour, IDamageable, IObservable
{

    public bool isDead;

    public float life = 100;
    public float maxLife = 100;
    [SerializeField]
    private float minLife = 0;


    List<IObserver> _allObservers = new List<IObserver>();

    void Awake()
    {
        EventManager.Subscribe("GameOver", Dead);
    }

    private void Start()
    {
        StartLifeFunc(life);
        EventManager.Subscribe("ResetLevel2",ResetLife);
    }
    public void StartLifeFunc(float loadLife)
    {
        life = loadLife;
        NotifyToObservers(life, maxLife);
    }

    public void AddLifeFunc(float dmg)
    {
        life += dmg;
        if (life > maxLife)
            life = maxLife;
        NotifyToObservers(life, maxLife);
    }
    public void SubtractLifeFunc(float dmg)
    {
        life -= dmg;
        NotifyToObservers(life, maxLife);
        
        AudioManager.instance.Play(AudioManager.Types.Damage);
        
        if (life <= 0)
        {
            EventManager.Trigger("GameOver");
        }
    }
    public void Dead(params object[] parameters)
    {
        isDead = true;
        //EventManager.Trigger("GameOver");
        //SoundManager.instance.Play(SoundManager.Types.Dead);
        life = minLife;
    }

    public void ResetLife(params object[] parameters)
    {
        life = maxLife/2;
    }

    public void Subscribe(IObserver obs)
    {
        if (!_allObservers.Contains(obs))
            _allObservers.Add(obs);

    }

    public void Unsubscribe(IObserver obs)
    {
        if (_allObservers.Contains(obs))
            _allObservers.Remove(obs);
    }

    public void NotifyToObservers(float value, float maxValue)
    {
        for (int i = 0; i < _allObservers.Count; i++)
            _allObservers[i].Notify(value, maxValue);
    }
}
