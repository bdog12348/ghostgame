using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject tutorialGO;
    GameManager gm;

    bool screenUp = true;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        gm.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (screenUp)
        {
            for(int i = 0; i < ReInput.players.playerCount; i++)
            {
                if (ReInput.players.GetPlayer(i).GetButtonDown("Jump"))
                {
                    screenUp = false;
                    tutorialGO.SetActive(false);
                    gm.Unpause();
                }
            }
        }
    }
}
