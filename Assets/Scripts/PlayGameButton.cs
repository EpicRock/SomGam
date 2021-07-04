using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGameButton : MonoBehaviour
{
    public Button button;
    public GameObject lobby;

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            Destroy(lobby);
            foreach (GameObject player in Player.AllPlayers)
            {
                player.GetComponent<Player>().Spawn(new Vector3(Random.Range(-8f, 8f), 0, 1));
            }
        });
    }
}
