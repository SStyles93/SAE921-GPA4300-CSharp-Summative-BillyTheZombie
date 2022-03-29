using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManagement : MonoBehaviour
{
    private PlayerStats _playerStats;

    [SerializeField] private Image _transitionImage;
    private Color _currentColor;
    [SerializeField] private float _transitionDuration = 1;

    private int _sceneIndex;
    [SerializeField] private bool _fadeIn;
    [SerializeField] private bool _fadeOut;

    public bool FadeIn { get => _fadeIn; set => _fadeIn = value; }
    public bool FadeOut { get => _fadeOut; set => _fadeOut = value; }
    public int SceneIndex { get => _sceneIndex; set => _sceneIndex = value; }
    public PlayerStats PlayerStats { get => _playerStats; set => _playerStats = value; }

    private void Start()
    {
        if (_transitionImage == null)
        {
            return;
        }
        _currentColor = _transitionImage.color = Color.black;
        _fadeIn = true;

    }

    private void Update()
    {
        if(_transitionImage == null)
        {
            return;
        }

        if(_fadeIn)
        {
            FadeInTransition();
        }
        if (_fadeOut)
        {
            _fadeIn = false;
            FadeOutTransition(_sceneIndex);
        }
    }

    private void FadeOutTransition(int SceneIndex)
    {
        _transitionImage.color = _currentColor;
        if(_currentColor != Color.black)
        {
            _currentColor = Color.Lerp(_currentColor, Color.black, Time.deltaTime / _transitionDuration);
            _playerStats.IsInvicible = true;
        }
        else
        {
            _fadeOut = false;
            ActivateScene(SceneIndex);
        }
    }
    private void FadeInTransition()
    {
        _transitionImage.color = _currentColor;
        if(_currentColor != Color.clear)
        {
            _currentColor = Color.Lerp(_currentColor, Color.clear, Time.deltaTime / _transitionDuration);
        }
        else
        {
            _fadeIn = false;
        }
    }

    /// <summary>
    /// Activates the wanted scene according to its index
    /// </summary>
    /// <param name="Index">Index of the wanted scene</param>
    public void ActivateScene(int Index)
    {
        _sceneIndex = Index;
        SceneManager.LoadScene(_sceneIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
 