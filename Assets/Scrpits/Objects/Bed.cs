using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField]
    private string _scene;

    private void Start()
    {
        _scene = "FirstScene";

        //si se cumplio tal cosa igualar _scene a nueva scene
    }
    public void OnCollisionExit(Collision with)
    {
        if (with.gameObject.tag == "Player")
        {
            switch (_scene)
            {
                case "FirstScene":
                    EventManager.Trigger("EventLGirl");
                    _scene = "Scene";
                    break;
            }
        }

        //sino poner directamente EventManager.Trigger(_scene);
        // fijarse qué conviene cuando agrega más cosas al código
        //dependiendo de cuántas veces muera o achievments cumplidos que cambia la escena
        //subscribirse a eventos que señalan muerte o achievments y sumarlos a un int, dependiendo del valor cambia la escenaw mostrada
        
    }
}
