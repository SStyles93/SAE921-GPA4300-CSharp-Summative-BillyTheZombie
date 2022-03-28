using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    //Reference Scripts
    private PlayerController _playerController;
    private PlayerMovement _playerMovement;
    private PlayerActions _playerActions;
    private PlayerStats _playerStats;

    //Reference Components
    private Animator _animator;
    //Animator Hashes
    private int _xPositionHash;
    private int _yPositionHash;
    private int _movementHash;
    private int _headbuttHash;

    public Animator Animator { get => _animator; private set => _animator = value; }

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _playerActions = GetComponentInParent<PlayerActions>();
        _playerStats = GetComponentInParent<PlayerStats>();
        _animator = GetComponent<Animator>();

        _xPositionHash = Animator.StringToHash("xPosition");
        _yPositionHash = Animator.StringToHash("yPosition");
        _movementHash = Animator.StringToHash("Movement");
        _headbuttHash = Animator.StringToHash("Headbutt");

    }
    private void Start()
    {
        _animator.speed = _playerStats.Speed/4.0f;
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
        if (_playerController.Look != Vector2.zero)
        {
            _animator.SetFloat(_xPositionHash, _playerController.Look.x);
            _animator.SetFloat(_yPositionHash, _playerController.Look.y);
        }
        else
        {
            _animator.SetFloat(_xPositionHash, _playerActions.Aim.transform.localPosition.x);
            _animator.SetFloat(_yPositionHash, _playerActions.Aim.transform.localPosition.y);
        }
    }
    /// <summary>
    /// Method used to update Animator Movement
    /// </summary>
    private void Move()
    {
        if (_playerController.Movement != Vector2.zero)
        {
            _animator.SetFloat(_movementHash, 1.0f);
        }
        else
        {
            _animator.SetFloat(_movementHash, 0.0f);
        }
    }
    
    /// <summary>
    /// Launch headbutt
    /// </summary>
    public void StartHeadbutt()
    {
        _animator.SetBool(_headbuttHash, true);
        _playerMovement.CanMove = false;

    }
    /// <summary>
    /// Method used to disable Headbutt bool in animator
    /// </summary>
    public void EndHeadbutt()
    {
        _animator.SetBool(_headbuttHash, false);
        _playerMovement.CanMove = true;
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
