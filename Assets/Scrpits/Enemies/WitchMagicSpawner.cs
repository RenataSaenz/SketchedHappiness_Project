using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchMagicSpawner : MonoBehaviour
{
    [SerializeField]
    private WitchMagic _magicPrefab;
    [SerializeField]
    private Transform _spawnPoint;

    public void SpawnMagic()
    {
        Instantiate(_magicPrefab, _spawnPoint.position, _spawnPoint.rotation);
    }
}
