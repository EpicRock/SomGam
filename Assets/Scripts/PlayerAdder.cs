using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

//Adding Players to PlayerHolder with user-defined keys
public class PlayerAdder : MonoBehaviour
{
    public Button button;
    public GameObject PlayerHolder;

    private Player NewPlayer;
    private bool WaitForKeyPress = false;
    private int KeyPass = 1;

    //Listens for button click to add new player
    public void Start()
    {
        button.onClick.AddListener(() => {
            NewPlayer = (Player)PlayerHolder.AddComponent(typeof(Player));
            WaitForKeyPress = true; //If true it waits for 3 presses of keys
        });
    }

    private void Update()
    {
        //Waits for button click and user input
        if (WaitForKeyPress && Input.anyKeyDown)
        {
            //Gets pressed key
            KeyCode key = GetKeyDown();

            //Checks for witch action the key was pressed
            switch (KeyPass)
            {
                case 1:
                {
                    NewPlayer.LeftKey = (KeyCode)key;
                    break;
                }
                case 2:
                {
                    NewPlayer.RightKey = (KeyCode)key;
                    break;
                }
                case 3:
                {
                    NewPlayer.AttackKey = (KeyCode)key;

                    //Ressets all data
                    KeyPass = 0;
                    WaitForKeyPress = false;
                    break;
                }
            }
            KeyPass++;
        }
    }

    private KeyCode GetKeyDown()
    {
        KeyCode KeyPressed = KeyCode.Alpha0;

        //Search for every possible key and check if it was pressed (yep, it has to be made like this...)
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key)) KeyPressed = key;
        }

        return KeyPressed;
    }
}
