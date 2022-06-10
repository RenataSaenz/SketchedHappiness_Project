using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerStats
{
    private static float _life;
    private static Vector3 _position;
    private static bool _loaded = false;

    public static float GetLife()
    {
        return _life;
    }
    public static void SetLife(float life)
    {
        _life = life;
    }
    
    public static Vector3 GetPosition()
    {
        return _position;
    }
    public static void SetPosition(Vector3 position)
    {
        _position = position;
    }

    public static void SetLoad(bool g)
    {
        _loaded = g;
    }

    public static bool GetLoad()
    {
        return _loaded;
    }
    
}
