using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    [SerializeField]
    private GameObject _toy;
    ToyManager _toyManager;
    [SerializeField]
    private ParticleSystem _particleSystem;
    void Start()
    {
        _toyManager = GameObject.Find("ManagerToy").GetComponent<ToyManager>();
        EventManager.Subscribe("ResetLevel2", ResetToys);
    }

    public void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.layer == Layers.PLAYER)
        {
            //_managerMood.AddPoints(100);
            
            var damageable = trig.transform.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.AddLifeFunc(100);
            }
            _toyManager.AddToys(1);
            AudioManager.instance.Play(AudioManager.Types.CollectedToy);
            Instantiate(_particleSystem, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
            
    }

    public void ResetToys(params object[] parameters)
    {
        gameObject.SetActive(true);
    }
}
