using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] VECTOR vector;
    [SerializeField] UnityEvent<VECTOR> OnInputDown;
    [SerializeField] UnityEvent<VECTOR> OnInputUp;

    bool isSelected = false;    // 누르는 중인가?

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        isSelected = false;
        OnInputUp?.Invoke(vector);
    }

    private void Update()
    {
        if (!isSelected)
            return;

        OnInputDown?.Invoke(vector);
    }

}
