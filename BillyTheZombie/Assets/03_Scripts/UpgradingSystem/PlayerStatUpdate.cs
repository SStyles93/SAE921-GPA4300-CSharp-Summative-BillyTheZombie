using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStatUpdate : MonoBehaviour, IMoveHandler, IEndDragHandler
{
    [SerializeField] private PlayerStatsSO _playerStats;

    private Slider _slider;

    private enum STATTYPE
    {
        PUSHPOWER,
        ARMDAMAGE,
        HEALTH,
        SPEED
    }
    [SerializeField] private STATTYPE statType;

    [SerializeField] private float stat;

    public float Stat { get => stat; set => stat = value; }

    public void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void Start()
    {
        switch (statType)
        {
            case STATTYPE.PUSHPOWER:
                stat = _playerStats._pushPowerPercentage;
                break;
            case STATTYPE.ARMDAMAGE:
                stat = _playerStats._armDamagePercentage;
                break;
            case STATTYPE.HEALTH:
                stat = _playerStats._healthPercentage;
                break;
            case STATTYPE.SPEED:
                stat = _playerStats._speedPercentage;
                break;
        }
    }

    public void UpdateStat()
    {
        switch (statType)
        {
            case STATTYPE.PUSHPOWER:
                _playerStats._pushPowerPercentage = stat;
                break;
            case STATTYPE.ARMDAMAGE:
                _playerStats._armDamagePercentage = stat;
                break;
            case STATTYPE.HEALTH:
                _playerStats._healthPercentage = stat;
                break;
            case STATTYPE.SPEED:
                _playerStats._speedPercentage = stat;
                break;
        }
    }
    public void UpdateSlider()
    {
        _slider.value = stat /100.0f;
    }

        /// <summary>
        /// Sets the behaviour of the slider OnMove
        /// </summary>
        /// <param name="eventData">The movement event</param>
        public void OnMove(AxisEventData eventData)
    {
        // override the slider value using our previousSliderValue and the desired step
        if (eventData.moveDir == MoveDirection.Left)
        {
            _slider.value = stat / 100.0f;
        }

        if (eventData.moveDir == MoveDirection.Right)
        {
            _slider.value = stat / 100.0f;
        }

        //// keep the slider value for future use
        //previousSliderValue = _slider.value;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //// keep the last slider value if the slider was dragged by mouse
        //previousSliderValue = _slider.value;
    }
}