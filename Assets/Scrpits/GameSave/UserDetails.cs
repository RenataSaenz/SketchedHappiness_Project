using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// [System.Serializable]
// public class UserDetails
// {
//     public string name;
//     public float life;
//     public Vector3 playerTransform;
//     //public List<Achievments> achievments = new List<Achievments>();
//     public string scene;
//     //public Dictionary<string, EventManager.EventReceiver> triggeredEvents = new Dictionary<string, EventManager.EventReceiver>();
//     // public enum Achievments
//     // {
//     //     None,
//     //     LevelRotatingRoomWon,
//     //     LevelPlaygroundWon,
//     //     LevelPlaygroundMovingDoorWon,
//     // }
//     
// }

[System.Serializable]
public class SavedGames 
{
    //public List<UserDetails> list;
    
    public string name;
    public float life;
    public Vector3 playerTransform;
    //public List<Achievments> achievments = new List<Achievments>();
    public string scene;
    //public Dictionary<string, EventManager.EventReceiver> triggeredEvents = new Dictionary<string, EventManager.EventReceiver>();
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
 
    public static SavedGames FromJson(string jsonData)
    {
        return JsonUtility.FromJson<SavedGames>(jsonData);
    }
}