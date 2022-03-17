using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonPrompt", menuName = "ScriptableObject/UIButtons", order = 2)]
public class UIButtonsSO : ScriptableObject
{
    public Sprite[] _keyboardSprites = new Sprite[3];
    public Sprite[] _gamepadSprites = new Sprite[3];
}
