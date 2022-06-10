using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Vector3 initialPos;

    [SerializeField] private Enemy _ghost;
    [SerializeField] private Enemy _mannequin;
    [SerializeField] private Enemy _witch;

    public Pool<Enemy> poolGhost;
    public Pool<Enemy> poolWitch;
    public Pool<Enemy> poolMannequin;

    public int counter = 0;
    public int totalEnemiesCounter;

    [SerializeField]private GameObject floorParent;
    
    private bool start;

    public Action ActiveEnemy;
    
    [SerializeField]private float timer = 1;
    [SerializeField]private float resetTimer;
    
    public static EnemyManager instance;
    
    Vector3 pos;
    float _xPos;
    float _yPos;
    float _zPos;
    private float _spawnPosition;
    [SerializeField]private Vector3 boxSize;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        pos = transform.position;
        ActiveEnemy = InstantiateEnemy;
        start = false;
        
        poolGhost = new Pool<Enemy>(CreateGhost, Enemy.TurnOff, Enemy.TurnOn, 0);
        poolWitch = new Pool<Enemy>(CreateWitch, Enemy.TurnOff, Enemy.TurnOn, 0);
        poolMannequin = new Pool<Enemy>(CreateMannequin, Enemy.TurnOff, Enemy.TurnOn, 0);
        
        resetTimer = timer;
    }

    private void FixedUpdate()
    {
        Await();

    }
    public void Await()
    {
        timer-=Time.deltaTime;
        
        if (timer <= 0)
        {
            ActiveEnemy();
            timer = resetTimer;
        }
    }
    void InstantiateEnemy()
    {
        _xPos = Random.Range(pos.x - boxSize.x/2, pos.x + boxSize.x/2); 
        _yPos = Random.Range(pos.y - boxSize.y/2, pos.y + boxSize.y/2); 
        _zPos = Random.Range(pos.z - boxSize.z/2, pos.z + boxSize.z/2);

        counter++;

        initialPos = new Vector3(_xPos, _yPos, _zPos);

          poolGhost.Get().SpawnEnemy(this, floorParent, initialPos);
            
        if (counter <= totalEnemiesCounter)
        {
            if(counter%2==1)
            {
                Debug.Log("mannequin");
                poolMannequin.Get().SpawnEnemy(this, floorParent, initialPos);
            }
            else
            {
                Debug.Log("witch");
                poolWitch.Get().SpawnEnemy(this, floorParent, initialPos);
            }
        }
        else
        {
            ActiveEnemy = delegate { };
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position,boxSize);
    }
    public void ReturnWitch (Enemy enemy)
    {
        poolWitch.Return(enemy);
    }
    public void ReturnMannequin (Enemy enemy)
    {
        poolMannequin.Return(enemy);
    }
    public void ReturnGhost (Enemy enemy)
    {
        poolGhost.Return(enemy);
    }
    public Enemy CreateMannequin()
    {
        EnemyFactory _factory = new EnemyFactory();
        return _factory.Create(_mannequin);
    } 
    public Enemy CreateGhost()
    {
        EnemyFactory _factory = new EnemyFactory();
        return _factory.Create(_ghost);
    } 
    public Enemy CreateWitch()
    {
        EnemyFactory _factory = new EnemyFactory();
        return _factory.Create(_witch);
    }

}