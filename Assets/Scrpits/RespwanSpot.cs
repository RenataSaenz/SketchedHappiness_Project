using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespwanSpot : MonoBehaviour
{
    [SerializeField]
    private static Vector3 _respawnPosition;
    
    public static void Position(RespwanSpot respwanSpot)
    {
        respwanSpot.gameObject.SetActive(false);
    }
    public static void TurnOff(RespwanSpot respwanSpot)
    { 
        respwanSpot.gameObject.SetActive(false);
    }
    public static void TurnOn(RespwanSpot respwanSpot)
    {
        _respawnPosition = respwanSpot.gameObject.transform.position;
    }
}
