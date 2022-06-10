using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Accessor
{
    public static int _counter;
    public static int _sumEnemiesDeadCounter;
    public static int _startCounter;
    public static bool isChatting = false;

    
    public static void StartCounter(int startCount)
    {
        _startCounter += startCount;
    }
    
    public static void RoomRotatorCounter(int timesEntered)
    {
        EventManager.Subscribe("StartsLevel2", ResetLevel2Counter);
        _counter += timesEntered;
        Debug.Log("Counter: " + _counter);
    }
    public static void ResetLevel2Counter(params object[] parameters)
    {
        _counter = 0;
        Debug.Log("Restart Counter: " + _counter);
    }
    public static void EnemiesDead(int enemiesCounter)
    {
        if (_startCounter == 1)
        {
            _sumEnemiesDeadCounter += enemiesCounter;
        }
    }

    public static void StartChatting(bool chat)
    {
        isChatting = chat;
    }
}
