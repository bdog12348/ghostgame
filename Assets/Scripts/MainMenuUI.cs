using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Rewired;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject[] screens;
    [SerializeField] GameObject[] characterSelectGOs;
    [SerializeField] GameObject startGameBannerGO;
    [SerializeField] Button[] mainButtons;
    [SerializeField] Button[] creditsButtons;

    public int maxPlayers = 4;

    List<PlayerMap> playerMap;
    List<int> addedRewiredJoysticks;
    int gamePlayerIdCounter = 0;
    int charLockedCounter = 0;

    bool canStartGame = false;
    bool movedHighlight = false;
    int currentlySelectedButton = 0;
    int currentSceneIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerMap = new List<PlayerMap>();
        addedRewiredJoysticks = new List<int>();
        foreach(GameObject go in characterSelectGOs)
        {
            CharacterSelecterHelper helper = go.GetComponent<CharacterSelecterHelper>();
            helper.m_CharacterLockedEvent.AddListener(CharacterLocked);
            helper.m_CharacterUnlockedEvent.AddListener(CharacterUnlocked);
        }
        HighlightCurrentButton();
    }

    // Update is called once per frame
    void Update()
    {
        bool verticalAxisPressed = false;
        for (int i = 0; i < ReInput.players.playerCount; i++)
        {
            if (currentSceneIndex == 0)
            {
                if (!movedHighlight)
                {
                    if (ReInput.players.GetPlayer(i).GetAxisRaw("Vertical") > 0)
                    {
                        if (currentlySelectedButton == 0)
                        {
                            currentlySelectedButton = 2;
                        }
                        else
                        {
                            currentlySelectedButton--;
                        }
                        movedHighlight = true;
                    }
                    else if (ReInput.players.GetPlayer(i).GetAxisRaw("Vertical") < 0)
                    {
                        if (currentlySelectedButton == 2)
                        {
                            currentlySelectedButton = 0;
                        }
                        else
                        {
                            currentlySelectedButton++;
                        }
                        movedHighlight = true;
                    }
                }

                if (ReInput.players.GetPlayer(i).GetButtonDown("Submit"))
                {
                    mainButtons[currentlySelectedButton].onClick.Invoke();
                }

                if (ReInput.players.GetPlayer(i).GetAxisRaw("Vertical") != 0)
                {
                    verticalAxisPressed = true;
                }
            }
            else if (currentSceneIndex == 1)
            {
                if (ReInput.players.GetPlayer(i).GetButtonDown("JoinGame"))
                {
                    if (!addedRewiredJoysticks.Contains(i))
                    {
                        AssignNextPlayer(i);
                    }

                    if (canStartGame)
                    {
                        LoadGame();
                    }
                }
            }
            else if (currentSceneIndex == 2)
            {
                if (ReInput.players.GetPlayer(i).GetButtonDown("Submit") || ReInput.players.GetPlayer(i).GetButtonDown("Cancel"))
                {
                    creditsButtons[currentlySelectedButton].onClick.Invoke();
                }
            }


        }

        HighlightCurrentButton();

        if (!verticalAxisPressed)
        {
            movedHighlight = false;
        }

        if (currentSceneIndex == 1 && charLockedCounter == gamePlayerIdCounter && gamePlayerIdCounter >= 1)
        {
            startGameBannerGO.SetActive(true);
            canStartGame = true;
        }
        else
        {
            canStartGame = false;
            startGameBannerGO.SetActive(false);
        }
    }

    public void ShowScreen(int number)
    {
        currentlySelectedButton = 0;
        for(int i = 0; i < screens.Length; i++)
        {
            if (i == number)
            {
                screens[i].SetActive(true);
                currentSceneIndex = i;
            }
            else
            {
                screens[i].SetActive(false);
            }
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void HighlightCurrentButton()
    {
        if (currentSceneIndex == 0)
        {
            for (int i = 0; i < mainButtons.Length; i++)
            {
                if (i == currentlySelectedButton)
                {
                    mainButtons[i].image.color = mainButtons[i].colors.highlightedColor;
                }
                else
                {
                    mainButtons[i].image.color = mainButtons[i].colors.normalColor;
                }
            }
        }
        else if (currentSceneIndex == 2)
        {
            creditsButtons[currentlySelectedButton].image.color = mainButtons[0].colors.highlightedColor;
        }
    }

    void AssignNextPlayer(int rewiredPlayerId)
    {
        if (playerMap.Count >= maxPlayers)
        {
            Debug.LogError("Max players reached!");
            return;
        }

        int gamePlayerId = GetNextGamePlayerId();

        addedRewiredJoysticks.Add(rewiredPlayerId);
        playerMap.Add(new PlayerMap(rewiredPlayerId, gamePlayerId));

        Player rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);

        characterSelectGOs[gamePlayerId].GetComponent<CharacterSelecterHelper>().player = rewiredPlayer;
        characterSelectGOs[gamePlayerId].GetComponent<CharacterSelecterHelper>().currentCharacterIndex = gamePlayerId;
        characterSelectGOs[gamePlayerId].GetComponent<CharacterSelecterHelper>().UpdateUI();
        characterSelectGOs[gamePlayerId].transform.GetChild(0).gameObject.SetActive(true);

        Debug.Log("Added Rewired Player id " + rewiredPlayerId + " to game player " + gamePlayerId);
    }
    int GetNextGamePlayerId()
    {
        return gamePlayerIdCounter++;
    }

    void CharacterLocked(int charNumber)
    {
        charLockedCounter++;
    }

    void CharacterUnlocked(int charNumber)
    {
        charLockedCounter--;
    }

    private class PlayerMap
    {
        public int rewiredPlayerId;
        public int gamePlayerId;

        public PlayerMap(int rewiredPlayerId, int gamePlayerId)
        {
            this.rewiredPlayerId = rewiredPlayerId;
            this.gamePlayerId = gamePlayerId;
        }
    }
}



