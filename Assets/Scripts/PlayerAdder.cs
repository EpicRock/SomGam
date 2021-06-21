using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayerAdder : MonoBehaviour
{
    public Button button;
    private bool WaitForKeyPress = false;
    private int KeyPass = 0;

    public void MouseDown()
    {
        WaitForKeyPress = true;
    }

    public void Start()
    {
        button.onClick.AddListener(MouseDown);
    }

    private void Update()
    {
        if (WaitForKeyPress)
        {
            //Create instance of Player

            if (Input.anyKeyDown)
            {
                //Run GetKeyDown for three KeyPass and push KeyCodes to Player

                WaitForKeyPress = false;
            }
        }
    }

    private KeyCode? GetKeyDown()
    {
        KeyCode? KeyPressed = null;

        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key)) KeyPressed = key;
        }

        return KeyPressed;
    }
}
