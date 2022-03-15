using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoctorAlbert : Interactable
{
    //Reference GameObjects
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _headSlider;

    //Reference Components
    [SerializeField] private EventSystem _eventSystem;

    //Reference ScriptableObject
    [SerializeField] private PlayerStatsSO _playerStatsSO;

    public void Start()
    {
        _canvas.gameObject.SetActive(false);
        
    }
    public override void Act()
    {
        _canvas.gameObject.SetActive(true);

        //Disable Player
        player.GetComponent<PlayerMovement>().CanMove = false;
        player.GetComponent<PlayerActions>().enabled = false;
        player.GetComponent<PlayerVisuals>().enabled = false;

        //Disables the "InfoBubble"
        _infoBubble.SetActive(false);

        //TODO : UI Nav
        _eventSystem.SetSelectedGameObject(_headSlider);

    }

    public override void StopActing()
    {
        _canvas.gameObject.SetActive(false);

        //Enable Player
        player.GetComponent<PlayerMovement>().CanMove = true;
        player.GetComponent<PlayerActions>().enabled = true;
        player.GetComponent<PlayerVisuals>().enabled = true;
        
        //Stops the UI Navigation
        _eventSystem.SetSelectedGameObject(null);
    }
}
