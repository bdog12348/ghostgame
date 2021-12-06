using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using MoreMountains.Feedbacks;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject tutorialGO;
    [SerializeField] GameObject hintBoxGO;
    [SerializeField] GameManager gameManager;
    GameManager gm;

    [SerializeField] MMFeedbackTMPTextReveal revealFeedback;
    [SerializeField] MMFeedbacks parentFeedback;
    [SerializeField] string[] tutorialText;

    int hintIndex = 0;
    bool gameStarted = false;

    bool screenUp = true;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();
        tutorialGO.SetActive(true);
        hintBoxGO.SetActive(false);
        gm.Pause();
        parentFeedback.Initialization(gameObject);
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
                    gm.StartGame();
                    gameStarted = true;
                }
            }
        }

        if (gameStarted)
        {
            hintBoxGO.SetActive(true);
            if (!parentFeedback.IsPlaying)
            {
                if (++hintIndex < tutorialText.Length)
                {
                    revealFeedback.NewText = tutorialText[hintIndex];
                    parentFeedback.PlayFeedbacks();
                } else
                {
                    hintBoxGO.SetActive(false);
                    gameManager.StartTimer();
                }
            }
        }
    }
}
