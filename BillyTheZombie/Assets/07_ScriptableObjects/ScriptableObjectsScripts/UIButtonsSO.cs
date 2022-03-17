using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonPrompt", menuName = "ScriptableObject/UIButtons", order = 2)]
public class UIButtonsSO : ScriptableObject
{
    [Tooltip("UI Sprites for Keyboard:\n[0]-Right\n[1]-Left\n[2]-Head")]
    public Sprite[] _keyboardSprites = new Sprite[3];
    [Tooltip("UI Sprites for Gamepad:\n[0]-Right\n[1]-Left\n[2]-Head")]
    public Sprite[] _gamepadSprites = new Sprite[3];
}
