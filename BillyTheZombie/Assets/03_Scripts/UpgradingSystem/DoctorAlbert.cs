using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoctorAlbert : Interactable
{
    //Reference GameObjects
    [Header("UI Objects")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject[] _sliders;
    [SerializeField] private GameObject[] _hightLights;

    //Reference Components
    [Header("Components")]
    [SerializeField] private EventSystem _eventSystem;

    //Reference ScriptableObjects
    [Header("Scriptable Objects")]
    [SerializeField] private GameStatsSO _gameStatsSO;
    [SerializeField] private PlayerStatsSO _playerStatsSO;

    //Variables
    [Header("Variables")]
    [Tooltip("The coefficient of MutagenPoints, 1 => 1pt/%")]
    [SerializeField] private float _pointsCoef = 1.0f;


    public void Start()
    {
        _canvas.gameObject.SetActive(false);

        foreach (GameObject slider in _sliders)
        {
            slider.GetComponentInChildren<Text>().text =
                $"{slider.name}";
        }
        
    }
    private void Update()
    {
        if (player != null)
        {
            UpdateUIButton();

            //Interact
            if (player.GetComponent<PlayerController>().ArmL && !hasInteracted)
            {
                Act();
                hasInteracted = true;
            }
        }
        if (_eventSystem.currentSelectedGameObject != null)
        {
            for (int i = 0; i < _sliders.Length; i++)
            {
                if (_eventSystem.currentSelectedGameObject == _sliders[i])
                {
                    HighlightSlider(_hightLights[i], true);
                    if (player.GetComponent<PlayerController>().ArmR)
                    {
                        AddPoints(_sliders[i]);
                    }
                    else if(player.GetComponent<PlayerController>().ArmL)
                    {
                        SubstractPoints(_sliders[i]);
                    }
                }
                else
                {
                    HighlightSlider(_hightLights[i], false);
                }
            }
        }
        UpdateSlidersVisuals();
    }

    public void HighlightSlider(GameObject highlightedImage, bool enable)
    {
        highlightedImage.SetActive(enable);
    }

    public override void Act()
    {
        _canvas.gameObject.SetActive(true);

        //Disable Player
        player.GetComponent<PlayerMovement>().CanMove = false;
        player.GetComponent<PlayerActions>().enabled = false;
        player.GetComponentInChildren<PlayerVisuals>().enabled = false;

        //Disables the "InfoBubble"
        _infoBubble.SetActive(false);

        //TODO : UI Nav
        _eventSystem.SetSelectedGameObject(_sliders[0]);

    }

    public override void StopActing()
    {
        _canvas.gameObject.SetActive(false);

        //Enable Player
        player.GetComponent<PlayerMovement>().CanMove = true;
        player.GetComponent<PlayerActions>().enabled = true;
        player.GetComponentInChildren<PlayerVisuals>().enabled = true;
        
        //Stops the UI Navigation
        _eventSystem.SetSelectedGameObject(null);
    }

    /// <summary>
    /// Updates the text contained in the Sliders
    /// </summary>
    private void UpdateSlidersVisuals()
    {
        foreach (GameObject slider in _sliders)
        {
            slider.GetComponentInChildren<Text>().text =
                $"{slider.GetComponent<Slider>().value * 100.0f} % {slider.name}";
        }
    }

    /// <summary>
    /// Adds MutagenPoints to a slider
    /// </summary>
    /// <param name="slider">The slider to add points to</param>
    private void AddPoints(GameObject slider)
    {
        if (_gameStatsSO.mutagenPoints > 0.0f 
            && slider.GetComponent<Slider>().value < 1.0f)
        {
            _gameStatsSO.mutagenPoints -= (0.1f * _pointsCoef);
            slider.GetComponent<PlayerStatUpdate>().Stat += (0.1f * _pointsCoef); 
            slider.GetComponent<Slider>().value += 0.1f/ 100.0f;
        }
    }

    /// <summary>
    /// Substracts MutagenPoints to a slider
    /// </summary>
    /// <param name="slider"></param>
    private void SubstractPoints(GameObject slider)
    {
        if (_gameStatsSO.mutagenPoints > 0.0f
            && slider.GetComponent<Slider>().value > 0.0f)
        {
            _gameStatsSO.mutagenPoints += (0.1f * _pointsCoef);
            slider.GetComponent<PlayerStatUpdate>().Stat -= (0.1f * _pointsCoef);
            slider.GetComponent<Slider>().value -= 0.1f / 100.0f;
        }
    }

    /// <summary>
    /// Reset all slider values
    /// </summary>
    public void ResetPoints()
    {
        for (int i = 0; i < _sliders.Length; i++)
        {
            _gameStatsSO.mutagenPoints += _sliders[i].GetComponent<Slider>().value * (100.0f * _pointsCoef);
            _sliders[i].GetComponent<PlayerStatUpdate>().Stat = 0.0f;
            _sliders[i].GetComponent<Slider>().value = 0.0f;
        }
    }

    public void SavePoints()
    {
        for (int i = 0; i < _sliders.Length; i++)
        {
            _sliders[i].GetComponent<PlayerStatUpdate>().UpdateStat();
        }
    }
}