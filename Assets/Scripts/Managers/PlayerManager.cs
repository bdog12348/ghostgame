using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerParent;
    [SerializeField] GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < DataHolder.PlayerMaps.Count; i++)
        {
            GameObject player_GO = Instantiate(playerPrefab, playerParent.position, Quaternion.identity, playerParent) as GameObject;
            PlayerController pc = player_GO.GetComponent<PlayerController>();
            pc.playerJoystick = DataHolder.Characters[i].ghostNumber;
            pc.SetPlayer(ReInput.players.GetPlayer(DataHolder.PlayerMaps[i].rewiredPlayerId));
            pc.ghostSprite = DataHolder.Characters[i].characterSprite;
        }
    }
}
