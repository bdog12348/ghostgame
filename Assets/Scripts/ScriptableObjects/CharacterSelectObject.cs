using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_", menuName = "ScriptableObjects/CharacterSelectScriptableObject", order = 1)]
public class CharacterSelectObject : ScriptableObject
{
    public Color characterColor;
    public Sprite characterSprite;
    public int ghostNumber;
}
