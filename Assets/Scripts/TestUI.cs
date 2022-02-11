using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestUI : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, ICancelHandler
{
    [SerializeField] Image image;
    [SerializeField] Color selectedColor;
    [SerializeField] Color deselectedColor;

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        image.color = selectedColor;
    }
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        image.color = deselectedColor;
    }

    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        Debug.Log("선택 : " + name);
    }
    void ICancelHandler.OnCancel(BaseEventData eventData)
    {
        Debug.Log("비선택 : " + name);
    }
}
