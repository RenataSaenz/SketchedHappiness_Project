using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Respawn : Player
{
   private List<int> _spots;
   public string _spotName;
   [SerializeField]
   private Text _spot1;
   [SerializeField]
   private Text _spot2;
   [SerializeField]
   private Text _spot3;
   [SerializeField]
   private Text _spot4;
   [SerializeField]
   private Text _spot5;
   [SerializeField]
   private GameObject _respawnSpotsPanel = null;
   private void Start()
   {
      Dictionary<int, string> RespawnSpots = new Dictionary<int, string>();

   }
   
   
   
   
   
}
