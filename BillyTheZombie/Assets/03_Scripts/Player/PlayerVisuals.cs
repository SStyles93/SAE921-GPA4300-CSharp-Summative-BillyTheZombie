using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    //Reference Scripts
    private PlayerController _controller;
    private PlayerActions _actions;

    //Reference Components
    private Animator _animator;

    public Animator Animator { get => _animator; private set => _animator = value; }

    private void Awake()
    {
        _controller = GetComponentInParent<PlayerController>();
        _actions = GetComponentInParent<PlayerActions>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Look();
        Move();
    }

    /// <summary>
    /// Method used to update Animator Positions
    /// </summary>
    private void Look()
    {
        if (_controller.Look != Vector2.zero)
        {
            _animator.SetFloat("xPosition", _controller.Look.x);
            _animator.SetFloat("yPosition", _controller.Look.y);
        }
        else
        {
            _animator.SetFloat("xPosition", _actions.Aim.transform.localPosition.x);
            _animator.SetFloat("yPosition", _actions.Aim.transform.localPosition.y);
        }
    }
    /// <summary>
    /// Method used to update Animator Movement
    /// </summary>
    private void Move()
    {
        if (_controller.Movement != Vector2.zero)
        {
            _animator.SetFloat("Movement", 1.0f);
        }
        else
        {
            _animator.SetFloat("Movement", 0.0f);
        }
    }

    public void Headbutt()
    {
        _animator.SetBool("Headbutt", true);

    }
    public void EndHeadbutt()
    {
        _animator.SetBool("Headbutt", false);
    }

    #region SpritePosition NOT USED

    //
    //    [Header("Sprite Bank")]
    //    [Tooltip("The players body sprites")]
    //    [SerializeField] private List<Sprite> _bodySprites;
    //    [Tooltip("The players rigth arm sprites")]
    //    [SerializeField] private List<Sprite> _rightArmSprites;
    //    [Tooltip("The players left arm sprites")]
    //    [SerializeField] private List<Sprite> _leftArmSprites;

    //    [Header("Player Renderers")]
    //    [Tooltip("The body renderer of the player")]
    //    [SerializeField] private SpriteRenderer _bodyRender;
    //    [Tooltip("The right arm renderer of the player")]
    //    [SerializeField] private SpriteRenderer _rightArmRender;
    //    [Tooltip("The left arm renderer of the player")]
    //    [SerializeField] private SpriteRenderer _leftArmRender;

    //    public enum PLAYERORIENTATION
    //    {
    //        NORTH,
    //        EAST,
    //        SOUTH,
    //        WEST
    //    }
    //    public int orientation;

    //    public void OrientPlayer(PLAYERORIENTATION orientationIdx)
    //    {
    //        orientation = (int)orientationIdx;
    //    }

    //    private void UpdatePlayerVisuals()
    //    {
    //        _bodyRender.sprite = _bodySprites[orientation];
    //        _rightArmRender.sprite = _rightArmSprites[orientation];
    //        _leftArmRender.sprite = _leftArmSprites[orientation];
    //    }

    #endregion
}
