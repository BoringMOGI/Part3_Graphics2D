using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Player.VECTOR vector;
    [SerializeField] UnityEvent<int> OnMobileInput;
    
    bool isSelected = false;    // 누르는 중인가?

    void Start()
    {

    }


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        isSelected = false;
    }
    private void Update()
    {
        if (!isSelected)
            return;

        OnMobileInput?.Invoke((int)vector);
    }

}
