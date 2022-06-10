using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{

    public KeyCode _Key;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_Key))
        {
            FadeToColor(_button.colors.pressedColor);
            _button.onClick.Invoke();
        }
        else if (Input.GetKeyUp(_Key))
        {
            FadeToColor(_button.colors.normalColor);
        }
    }

    public void OnItemClicked()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

    void FadeToColor(Color color)
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
    }
}
