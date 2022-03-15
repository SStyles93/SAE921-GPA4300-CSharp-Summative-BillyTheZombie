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

    public void Start()
    {
        _canvas.gameObject.SetActive(false);
    }
    public override void Act()
    {
        Debug.Log("Acting");
        _canvas.gameObject.SetActive(true);

        //Disable Player
        player.GetComponent<PlayerMovement>().CanMove = false;
        player.GetComponent<PlayerActions>().enabled = false;
        player.GetComponent<PlayerVisuals>().enabled = false;

        //Enables the "InfoBubble"
        _infoBubble.SetActive(false);

        _eventSystem.SetSelectedGameObject(_returnButton);
        
    }

    public override void StopActing()
    {
        Debug.Log("Stops Acting");
        _canvas.gameObject.SetActive(false);

        //Enable Player
        player.GetComponent<PlayerMovement>().CanMove = true;
        player.GetComponent<PlayerActions>().enabled = true;
        player.GetComponent<PlayerVisuals>().enabled = true;

        _eventSystem.SetSelectedGameObject(null);
    }
}
