using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T>
{
    private List<T> _uninstantiated = new List<T>();
    private Func <T> _create;
    private Action<T> _turnOff;
    private Action<T> _turnOn;

    public Pool(Func<T> create, Action<T> turnOff, Action<T> turnOn, int amount)
    {
        _create = create;
        _turnOff = turnOff;
        _turnOn = turnOn;

        for (var i = 0; i < amount; i++)
        {
            var element = _create();
            _uninstantiated.Add(element);
        }
    }

    public T Get()
    {
        if (_uninstantiated.Count > 0)
        {
            var element = _uninstantiated[0];
            _uninstantiated.RemoveAt(0);
            _turnOn(element);
            return element;
        }

        var instance = _create();
        _turnOn(instance);
        return instance;
    }

    public void Return(T element)
    {
        _uninstantiated.Add(element);
        _turnOff(element);
    }

}