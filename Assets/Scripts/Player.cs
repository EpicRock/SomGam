using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Pushing players to this list is done in PlayerAdder script
    public static List<GameObject> AllPlayers = new List<GameObject>();

    public SpriteRenderer spriteRenderer;

    //All of these fields are set up by PlayerAdder script
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode DashKey;
    public KeyCode ActionKey;

    public void Spawn (Vector3 SpawnPoint)
    {
        transform.position = SpawnPoint;
        spriteRenderer.enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
    }
}
