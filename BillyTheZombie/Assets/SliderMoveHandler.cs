using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SliderMoveHandler : MonoBehaviour, IMoveHandler, IEndDragHandler
{
    Slider slider;
    //float previousSliderValue = 0f;

    void Awake()
    {
        slider = GetComponent<Slider>();
        //if (slider)
        //    previousSliderValue = slider.value;
    }

    public void OnMove(AxisEventData eventData)
    {
        // override the slider value using our previousSliderValue and the desired step
        if (eventData.moveDir == MoveDirection.Left)
        {
            slider.value = slider.value;
        }

        if (eventData.moveDir == MoveDirection.Right)
        {
            slider.value = slider.value;
        }

        //// keep the slider value for future use
        //previousSliderValue = slider.value;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //// keep the last slider value if the slider was dragged by mouse
        //previousSliderValue = slider.value;
    }
}
