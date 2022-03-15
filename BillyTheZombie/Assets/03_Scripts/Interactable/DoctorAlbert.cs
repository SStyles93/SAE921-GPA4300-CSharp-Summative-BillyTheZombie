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
    [SerializeField] private List<GameObject> _sliders;

    //Reference Components
    [Header("Components")]
    [SerializeField] private EventSystem _eventSystem;

    //Reference ScriptableObjects
    [Header("Scriptable Objects")]
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private GameStatsSO _gameStatsSO;

    public void Start()
    {
        _canvas.gameObject.SetActive(false);
        foreach(GameObject slider in _sliders)
        {
            slider.GetComponentInChildren<Text>().text =
                $"{slider.name}";
        }
        
    }
    private void Update()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerController>().Head && !hasInteracted)
            {
                Act();
                hasInteracted = true;
            }
        }
        if(_eventSystem.currentSelectedGameObject != null)
        {
            for (int i = 0; i < _sliders.Count; i++)
            {
                if(_eventSystem.currentSelectedGameObject == _sliders[i])
                {
                    AddPoints(_sliders[i]);
                }
            }
        }
        UpdateSlidersVisuals();
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
        if (player.GetComponent<PlayerController>().Head 
            && _gameStatsSO.mutagenPoints > 0.0f 
            && slider.GetComponent<Slider>().value < 1.0f)
        {
            _gameStatsSO.mutagenPoints -= 1.0f;
            slider.GetComponent<Slider>().value += 1.0f/ 100.0f;
        }
    }

    /// <summary>
    /// Reset all slider values
    /// </summary>
    public void ResetPoints()
    {
        foreach(GameObject slider in _sliders)
        {
            _gameStatsSO.mutagenPoints += slider.GetComponent<Slider>().value * 100.0f;
            slider.GetComponent<Slider>().value = 0.0f;
        }
    }
}
