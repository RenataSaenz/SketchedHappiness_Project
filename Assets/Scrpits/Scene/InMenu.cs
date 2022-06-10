using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject actualMenu;
    public void OnClick()
    {
        menu.SetActive(true);
        actualMenu.SetActive(false);
    }
}

