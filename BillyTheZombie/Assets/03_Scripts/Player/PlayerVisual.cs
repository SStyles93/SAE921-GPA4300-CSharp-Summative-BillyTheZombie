using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [Header("Sprite Bank")]
    [Tooltip("The players body sprites")]
    [SerializeField] private List<Sprite> _bodySprites;
    [Tooltip("The players rigth arm sprites")]
    [SerializeField] private List<Sprite> _rightArmSprites;
    [Tooltip("The players left arm sprites")]
    [SerializeField] private List<Sprite> _leftArmSprites;

    [Header("Player Renderers")]
    [Tooltip("The body renderer of the player")]
    [SerializeField] private SpriteRenderer _bodyRender;
    [Tooltip("The right arm renderer of the player")]
    [SerializeField] private SpriteRenderer _rightArmRender;
    [Tooltip("The left arm renderer of the player")]
    [SerializeField] private SpriteRenderer _leftArmRender;

    public enum PLAYERPOSITIONS
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdatePlayerVisuals(int playersPositionIndex)
    {
        _bodyRender.sprite = _bodySprites[playersPositionIndex];
        _rightArmRender.sprite = _rightArmSprites[playersPositionIndex];
        _leftArmRender.sprite = _leftArmSprites[playersPositionIndex];
    }
}
