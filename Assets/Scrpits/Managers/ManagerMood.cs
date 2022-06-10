/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerMood : MonoBehaviour
{
  #region Variables

  [System.NonSerialized]
  public float points;

  public float maxPoints;
  public float minPoints;

  #endregion

  public void Awake()
  {
      points = maxPoints / 2;
  }

  public void AddPoints(float value)
  {
      points += value;
      if (points > maxPoints)
          points = maxPoints;
  }
    
  public void ResetLife()
    {
        points = maxPoints / 2;
    }
    

    public void SubtractPoints(float value)
    {
      points -= value;
        AudioManager.instance.Play(AudioManager.Types.Damage);
        if (points < minPoints)
          points = minPoints;
    }

  /// <summary>
  /// metodo para devolver los puntos acumulados
  /// </summary>
  /// <returns></returns>
  public float ReturnPoints()
  {
      return points;
  }
}*/
