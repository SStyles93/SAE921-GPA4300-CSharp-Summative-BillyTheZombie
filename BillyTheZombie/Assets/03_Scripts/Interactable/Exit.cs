using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Player;
public class Exit : Interactable
{
    [Header("UI Objects")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _returnButton;

    [SerializeField] private SpriteRenderer _doorSpriteRender;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _doorSound;

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
        
        _eventSystem.SetSelectedGameObject(_returnButton);
        _audioSource.clip = _doorSound;
        if(!_audioSource.isPlaying)
        _audioSource.Play();
        
    }

    public override void StopActing()
    {
        _canvas.gameObject.SetActive(false);

        //Enable Player
        player.GetComponent<PlayerMovement>().CanMove = true;
        player.GetComponent<PlayerActions>().enabled = true;
        player.GetComponentInChildren<PlayerVisuals>().enabled = true;

        _eventSystem.SetSelectedGameObject(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            player = collision.gameObject;

            _doorSpriteRender.enabled = true;

            //Enables the "InfoBubble"
            _infoBubble.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player != null)
        {
            StopActing();
            player = null;
            hasInteracted = false;
            //Disables the "InfoBubble"
            _infoBubble.SetActive(false);

            _doorSpriteRender.enabled = false;
        }

    }
}
