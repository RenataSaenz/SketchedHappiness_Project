using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void OnStart();
    void OnUpdate();
    void OnExit();
}

public class BlankState : IState
{
    public void OnExit()
    {
    }
    public void OnUpdate()
    {
    }
    public void OnStart()
    {
    }

    
}