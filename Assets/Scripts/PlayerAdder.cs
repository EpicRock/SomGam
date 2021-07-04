using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.EventSystems;

//Adding Players to PlayerHolder with user-defined keys
public class PlayerAdder : MonoBehaviour
{
    public Button button;
    public GameObject PlayerPrefab;
    public EventSystem eventSystem;
    public TextMeshProUGUI ButtonText;

    //Text slots for every key
    public TextMeshProUGUI LeftKeyTMP;
    public TextMeshProUGUI RightKeyTMP;
    public TextMeshProUGUI DashKeyTMP;
    public TextMeshProUGUI ActionKeyTMP;

    private Player player = null;
    private bool WaitForKeyPress = false;
    private int KeyPass = 1;
    private static List<KeyCode> BlackList = new List<KeyCode>();

    public void Start()
    {
        //Standard blacklisted keys
        if (BlackList.Count == 0) BlackList.Add(KeyCode.Mouse0);

        //Wait for button click
        button.onClick.AddListener(() => {
            //Adding or removing player
            if (player == null)
            {
                player = Instantiate(PlayerPrefab).GetComponent<Player>();
                Player.AllPlayers.Add(player.gameObject);
                WaitForKeyPress = true; //If true it waits for 4 presses of keys
                eventSystem.enabled = false; //UI freezes
                ButtonText.text = "Remove";
            }
            else
            {
                //Removing all used keys from black list so they can be used again
                BlackList.Remove(player.LeftKey);
                BlackList.Remove(player.RightKey);
                BlackList.Remove(player.DashKey);
                BlackList.Remove(player.ActionKey);

                //Removing keys from gui text slots
                LeftKeyTMP.text = "";
                RightKeyTMP.text = "";
                DashKeyTMP.text = "";
                ActionKeyTMP.text = "";

                //Cleaning
                Player.AllPlayers.Remove(player.gameObject);
                Destroy(player.gameObject);
                player = null;
                ButtonText.text = "Add player";
            }
        });
    }

    private void Update()
    {
        //Waits for button click and user input
        if (WaitForKeyPress && Input.anyKeyDown)
        {
            //Gets pressed key
            KeyCode key = GetKeyDown();

            if (KeyIsValid(key))
            {
                //Checks for witch action the key was pressed
                switch (KeyPass)
                {
                    case 1:
                        player.LeftKey = key;
                        LeftKeyTMP.text = Enum.GetName(typeof(KeyCode), key);
                        break;
                    case 2:
                        player.RightKey = key;
                        RightKeyTMP.text = Enum.GetName(typeof(KeyCode), key);
                        break;
                    case 3:
                        player.DashKey = key;
                        DashKeyTMP.text = Enum.GetName(typeof(KeyCode), key);
                        break;
                    case 4:
                        player.ActionKey = key;
                        ActionKeyTMP.text = Enum.GetName(typeof(KeyCode), key);

                        //Unfreeze UI
                        eventSystem.enabled = true;

                        //Ressets all data
                        KeyPass = 0;
                        WaitForKeyPress = false;
                        break;
                }

                //Increse pass to get next key and blacklist pressed curent key so it cant be used later
                BlackList.Add(key);
                KeyPass++;
            }
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

    private bool KeyIsValid (KeyCode key)
    {
        //Search if key is in BlackList
        foreach (KeyCode BlackListed in BlackList)
        {
            if (key == BlackListed) return false;
        }

        return true;
    }
}
