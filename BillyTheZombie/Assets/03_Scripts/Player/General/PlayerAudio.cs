using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    //Reference Scripts
    [Header("Reference Scripts")]
    [SerializeField] private PlayerController _playerController;

    //Reference Components
    [Header("Reference Components")]
    [SerializeField] private AudioSource _audioSource;

    //Audio Clips
    [Header("Audio Clips")]
    [SerializeField] private AudioClip _footStepsClip;
    [SerializeField] private AudioClip _armThrowClip;
    [SerializeField] private AudioClip _headbuttClip;


    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //FootSteps
        if(_playerController.Movement != Vector2.zero)
        {
            PlayClip(_footStepsClip);
        }
        //ArmThrow
        if(_playerController.ArmL || _playerController.ArmR)
        {
            PlayClip(_armThrowClip);
        }
        //HeadButt
        if (_playerController.Head)
        {
            PlayClip(_headbuttClip);
        }
    }

    /// <summary>
    /// Plays the Audio clip given to the method
    /// </summary>
    /// <param name="clip">The clip to play</param>
    private void PlayClip(AudioClip clip)
    {
        _audioSource.clip = clip;
        if(!_audioSource.isPlaying)
        _audioSource.Play();
    }
}
