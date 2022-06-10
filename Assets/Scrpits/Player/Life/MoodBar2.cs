using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Serialization;

public class MoodBar2 : MonoBehaviour, IObserver
{
    [FormerlySerializedAs("lifeBar")] public Image moodBar;
    IObservable _playerToCopy;
    public PlayerMood playerMood;
    public Text moodText;
    private void Start()
    {
        _playerToCopy = playerMood;
        _playerToCopy.Subscribe(this);
    }

    void BarUpdate(float _mood, float _maxMood)
    {
        moodBar.fillAmount = (_mood / _maxMood);
        Color moodColor = Color.Lerp(Color.black, Color.white, (_mood / _maxMood));
        moodBar.color = moodColor;
        moodText.text = "Sanity: " + _mood + "%";
        
    }
    public void Notify(float value, float maxValue)
    {
        BarUpdate(value, maxValue);
    }

}
