using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.Events;

[System.Serializable]
public class CharacterLockedEvent : UnityEvent<int>
{
}

[System.Serializable]
public class CharacterUnlockedEvent : UnityEvent<int>
{
}

public class CharacterSelecterHelper : MonoBehaviour
{
    //Events
    public CharacterLockedEvent m_CharacterLockedEvent;
    public CharacterUnlockedEvent m_CharacterUnlockedEvent;

    [SerializeField] CharacterSelectObject[] characters;
    [HideInInspector] public Player player = null;

    [Header("UI Stuff")]
    [SerializeField] Image characterBG;
    [SerializeField] Image characterSprite;
    [SerializeField] TextMeshProUGUI characterName;

    [HideInInspector] public int currentCharacterIndex = 0;

    // Private vars
    CharacterSelectObject currentCharacter;
    bool[] lockedCharacters = new bool[4];
    bool switchedCharacter = false;
    bool lockedIn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_CharacterLockedEvent == null)
            m_CharacterLockedEvent = new CharacterLockedEvent();

        if (m_CharacterUnlockedEvent == null)
            m_CharacterUnlockedEvent = new CharacterUnlockedEvent();

        foreach (CharacterSelecterHelper helper in FindObjectsOfType<CharacterSelecterHelper>())
        {
            if (!helper.Equals(this))
            {
                helper.m_CharacterLockedEvent.AddListener(SetLockEventResponse);
                helper.m_CharacterUnlockedEvent.AddListener(SetUnlockEventResponse);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.GetButtonDown("Submit"))
            {
                LockIn();
            }

            if (player.GetButtonDown("Cancel"))
            {
                UnlockIn();
            }

            if (!lockedIn)
            {
                if (!switchedCharacter)
                {
                    if (player.GetAxisRaw("Horizontal") > 0)
                    {
                        NextCharacter();
                        switchedCharacter = true;
                    }else if (player.GetAxisRaw("Horizontal") < 0)
                    {
                        PreviousCharacter();
                        switchedCharacter = true;
                    }
                }
                else
                {
                    if (player.GetAxisRaw("Horizontal") == 0)
                    {
                        switchedCharacter = false;
                    }
                }
            }
        }
    }

    void SetLockEventResponse(int characterNumber)
    {
        if (characterNumber == currentCharacterIndex)
        {
            currentCharacterIndex = NextAvailableCharacterIndex(currentCharacterIndex);
            UpdateUI();
        }
        lockedCharacters[characterNumber] = true;
    }

    void SetUnlockEventResponse(int characterNumber)
    {
        lockedCharacters[characterNumber] = false;
    }

    void LockIn()
    {
        if (lockedCharacters[currentCharacterIndex] != true)
        {
            characterSprite.color = Color.white;
            lockedCharacters[currentCharacterIndex] = true;
            m_CharacterLockedEvent.Invoke(currentCharacterIndex);
            lockedIn = true;
        }
    }

    void UnlockIn()
    {
        if (lockedIn)
        {
            characterSprite.color = new Color(1, 1, 1, .5f);
            lockedCharacters[currentCharacterIndex] = false;
            m_CharacterUnlockedEvent.Invoke(currentCharacterIndex);
            lockedIn = false;
        }
    }

    void PreviousCharacter()
    {
        currentCharacterIndex = PreviousAvailableCharacterIndex(currentCharacterIndex);
        UpdateUI();
    }

    void NextCharacter()
    {
        currentCharacterIndex = NextAvailableCharacterIndex(currentCharacterIndex);
        UpdateUI();
    }

    int NextAvailableCharacterIndex(int charIndex, int depth = 0)
    {
        if (charIndex == 3)
        {
            charIndex = 0;
        }else
        {
            charIndex++;
        }

        if (lockedCharacters[charIndex] != false && depth < 20)
        {
            charIndex = NextAvailableCharacterIndex(charIndex, ++depth);
        }
        return charIndex;
    }

    int PreviousAvailableCharacterIndex(int charIndex, int depth = 0)
    {
        if (charIndex == 0)
        {
            charIndex = 3;
        }
        else
        {
            charIndex--;
        }

        if (lockedCharacters[charIndex] != false && depth < 20)
        {
            charIndex = NextAvailableCharacterIndex(charIndex, ++depth);
        }
        return charIndex;
    }

    public void UpdateUI()
    {
        currentCharacter = characters[currentCharacterIndex];
        characterBG.color = currentCharacter.characterColor;
        characterSprite.sprite = currentCharacter.characterSprite;
        characterName.text = currentCharacter.name;
    }
}
