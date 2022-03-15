using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected GameObject player;
    private bool hasInteracted = false;

    [SerializeField] protected GameObject _infoBubble;

    public void Awake()
    {
        _infoBubble.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            player = collision.gameObject;

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
        }

    }
    private void Update()
    {
        if(player != null)
        {
            if (player.GetComponent<PlayerController>().Head && !hasInteracted)
            {
                Act();
                hasInteracted = true;
            }
        }
    }

    /// <summary>
    /// Acts with the Interactable
    /// </summary>
    public abstract void Act();
    /// <summary>
    /// Stops acting with the Interacable
    /// </summary>
    public abstract void StopActing();
}
