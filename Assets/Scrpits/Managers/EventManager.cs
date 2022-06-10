using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class EventManager
{
    public delegate void EventReceiver(params object[] parameters);
    static Dictionary<string, EventReceiver> _events = new Dictionary<string, EventReceiver>();
    //public static Dictionary<string, EventReceiver> _triggeredEvents = new Dictionary<string, EventReceiver>();
    public static void Subscribe(string eventType, EventReceiver method)
    {
        if (_events.ContainsKey(eventType))
            _events[eventType] += method;
        else
            _events.Add(eventType, method);
        
         //if ( _triggeredEvents.ContainsKey(eventType))
            // _events[eventType] -= method;
    }
    public static void UnSubscribe(string eventType, EventReceiver method)
    {
        if (_events.ContainsKey(eventType))
        {
            _events[eventType] -= method;

            if (_events[eventType] == null)
                _events.Remove(eventType);
            
           // if (_triggeredEvents.ContainsKey(eventType))
               // _triggeredEvents[eventType] += method;
            //else
                //_triggeredEvents.Add(eventType, method);
        }
    }
    public static void Trigger(string eventType, params object[] parameters)
    {
        if (_events.ContainsKey(eventType))
            _events[eventType](parameters);
        else
            Debug.Log("No existe el metodo");
    }

    public static void Clear()
    {
        _events.Clear();
    }
    
}
