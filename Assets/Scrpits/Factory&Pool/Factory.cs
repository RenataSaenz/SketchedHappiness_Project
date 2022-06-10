using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory<T>
{
    T Create(T prefab);
}

public class EnemyFactory : IFactory<Enemy>
{
    public Enemy Create(Enemy prefab)
    {
        var enemy = Object.Instantiate(prefab);
        return enemy;
    }
}