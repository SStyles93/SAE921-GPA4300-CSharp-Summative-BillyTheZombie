using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //Reference Scripts
    [Header("Player Scripts")]
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private PlayerActions _playerActions;
    [SerializeField] private PlayerController _playerController;

    //Reference Components
    [Header("Player Health UI")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Gradient _healthGradient;
    [SerializeField] private Image _healthFill;
    [SerializeField] private Image _headImage;
    [SerializeField] private List<Sprite> _headSprites;

    [Header("Player Actions UI")]
    [SerializeField] private Color _blockedColor = Color.gray;
    private Color _normalColor = Color.white;
    [Tooltip("List of ArmImages: [0]-Right || [1]-Left || [2]-Head")]
    [SerializeField] private List<Image> _bodyImages;
    [Tooltip("List of ButtonImages: [0]-Right || [1]-Left || [2]-Head")]
    [SerializeField] private List<Image> _buttonImages;

    private void Awake()
    {
        _playerStats = GetComponentInParent<PlayerStats>();
        _playerActions = GetComponentInParent<PlayerActions>();
        _playerController = GetComponentInParent<PlayerController>();

        _healthSlider = GetComponentInChildren<Slider>();
    }
    private void Start()
    {
        _healthSlider.maxValue = _playerStats.MaxHealth;
        _healthSlider.value = _playerStats.Health;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        UpdateActionsUI();
        UpdateButtonsUI();
    }

    /// <summary>
    /// Updates the player's healthbar UI
    /// </summary>
    private void UpdateHealthBar()
    {
        _healthSlider.value = _playerStats.Health;
        _healthFill.color = _healthGradient.Evaluate(_healthSlider.value / _healthSlider.maxValue);
        if (_playerStats.Health <= (_playerStats.MaxHealth * 0.5f))
        {
            _headImage.sprite = _headSprites[1];
        }
        else
        {
            _headImage.sprite = _headSprites[0];
        }
    }
    
    /// <summary>
    /// Updates the player's actions UI according to PlayerActions
    /// </summary>
    private void UpdateActionsUI()
    {
        //If CanThrow => normalColor

        //Right arm
        _bodyImages[(int)ARMSIDE.RIGHT].color = 
            _playerActions.CanThrow[(int)ARMSIDE.RIGHT] ? _normalColor : _blockedColor;

        //Left arm
        _bodyImages[(int)ARMSIDE.LEFT].color =
            _playerActions.CanThrow[(int)ARMSIDE.LEFT] ? _normalColor : _blockedColor;

        //Head
        _bodyImages[2].color =
            _playerActions.CanHeadbutt ? _normalColor : _blockedColor;
    }

    /// <summary>
    /// Updated the button UI according to PlayerController
    /// </summary>
    private void UpdateButtonsUI()
    {
        // If button pressed => blockedColor

        //Right arm trigger
        _buttonImages[(int)ARMSIDE.RIGHT].color =
            _playerController.ArmR ? _blockedColor : _normalColor;

        //Left arm trigger
        _buttonImages[(int)ARMSIDE.LEFT].color =
            _playerController.ArmL ? _blockedColor : _normalColor;

        //Head button
        _buttonImages[2].color =
            _playerController.Head ? _blockedColor : _normalColor;
    }

}
