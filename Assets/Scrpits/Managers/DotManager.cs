using System.Collections.Generic;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    public EntityPositionList[] dots;
    public static DotManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    [System.Serializable]
    public class EntityPositionList
    {
        public string name;
        public List<Transform> dots;
    }
}