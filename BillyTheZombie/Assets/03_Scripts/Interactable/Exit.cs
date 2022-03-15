using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Exit : Interactable
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _returnButton;

    [SerializeField] private SpriteRenderer _doorSpriteRender;

    public void Start()
    {
        _canvas.gameObject.SetActive(false);
        _doorSpriteRender.enabled = false;
    }
    public override void Act()
    {
        _canvas.gameObject.SetActive(true);

        //Disable Player
        player.GetComponent<PlayerMovement>().CanMove = false;
        player.GetComponent<PlayerActions>().enabled = false;
        player.GetComponentInChildren<PlayerVisuals>().enabled = false;

        //Enables the "InfoBubble"
        _infoBubble.SetActive(false);
        
        _doorSpriteRender.enabled = true;

        _eventSystem.SetSelectedGameObject(_returnButton);
        
    }

    public override void StopActing()
    {
        _canvas.gameObject.SetActive(false);

        //Enable Player
        player.GetComponent<PlayerMovement>().CanMove = true;
        player.GetComponent<PlayerActions>().enabled = true;
        player.GetComponentInChildren<PlayerVisuals>().enabled = true;

        _doorSpriteRender.enabled = false;

        _eventSystem.SetSelectedGameObject(null);
    }
}
