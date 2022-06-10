using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get { return _instance; }
        
    }

    private NewControlsCamera playerControls;

    private void Awake()
    {
        if (_instance != null && _instance != this) { Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        playerControls = new NewControlsCamera();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

}
